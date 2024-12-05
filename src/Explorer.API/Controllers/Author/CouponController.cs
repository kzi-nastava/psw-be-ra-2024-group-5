using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public.Author;
using Explorer.Payments.Core.Domain;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.TourLifecycle;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.API.Public.Author;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author
{
	[Authorize(Policy = "authorPolicy")]
	[Route("api/coupon")]
	public class CouponController : BaseApiController
	{
		private readonly ICouponService _couponService;
		private readonly ITourService _tourService;

		public CouponController(ICouponService couponService,ITourService tourService)
		{
			_couponService = couponService;
			_tourService = tourService;
		}

		[HttpPost("create")]
		public async Task<ActionResult<CouponDto>> Create([FromBody] CouponDto couponDto) 
		{
			if (!ModelState.IsValid) 
				return BadRequest(ModelState);

			if (couponDto.TourIds == null || !couponDto.TourIds.Any())
			{
				return BadRequest("At least one tour must be selected.");
			}

			var result = await this._couponService.Create(couponDto);
			
			if (!result.IsSuccess)
			{
				return BadRequest(result.Errors.FirstOrDefault()?.Message);
			}

			return Ok(result.Value);

		}

		[HttpDelete("delete/{id:long}")]
		public ActionResult Delete(long id)
		{
			var result = _couponService.Delete(id);

			if (!result.IsSuccess)
			{
				return NotFound(result.Errors.FirstOrDefault()?.Message);
			}

			return NoContent(); // HTTP 204 - Uspešno obrisano
		}

		[HttpPut("update/{id:long}")]
		public ActionResult Update(long id, [FromBody] CouponDto couponDto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var result = _couponService.Update(id, couponDto);

			if (!result.IsSuccess)
			{
				return NotFound(result.Errors.FirstOrDefault()?.Message);
			}

			return Ok(result.Value); // Vraća ažurirani kupon
		}
		[HttpGet("getAll")]
		public ActionResult GetAll()
		{
			var result = _couponService.GetAll();

			if (!result.IsSuccess)
			{
				return NotFound(result.Errors.FirstOrDefault()?.Message);
			}

			return Ok(result.Value); // Vraća listu kupona
		}

		[HttpGet("code")]
		[AllowAnonymous]
		public ActionResult<CouponDto> GetByCode(string code)
		{
			var result = _couponService.GetByCode(code);

			if (!result.IsSuccess)
			{
				return NotFound(result.Errors.FirstOrDefault()?.Message);
			}

			return Ok(result.Value); 
		}


		[HttpGet("tours")]
		public async Task<ActionResult<List<TourDto>>> GetToursByIds([FromQuery] List<long> tourIds)
		{
			if (tourIds == null || !tourIds.Any())
			{
				return BadRequest("No tour IDs provided.");
			}

			var tours = await _tourService.GetToursByIds(tourIds);

			if (tours == null || !tours.Any())
			{
				return NotFound("No tours found for the provided IDs.");
			}

			return Ok(tours);
		}
	}

}
