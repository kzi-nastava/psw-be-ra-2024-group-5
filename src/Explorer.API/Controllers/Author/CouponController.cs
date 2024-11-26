using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public.Author;
using Explorer.Tours.API.Dtos;
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

		public CouponController(ICouponService couponService)
		{
			_couponService = couponService;
		}

		[HttpPost("create")]
		public ActionResult<CouponDto> Create([FromBody] CouponDto couponDto) 
		{
			if (!ModelState.IsValid) 
				return BadRequest(ModelState);

			var result = this._couponService.Create(couponDto);
			
			if (!result.IsSuccess)
			{
				return BadRequest(result.Errors.FirstOrDefault()?.Message);
			}

			return Ok(result.Value);

		}
	}
}
