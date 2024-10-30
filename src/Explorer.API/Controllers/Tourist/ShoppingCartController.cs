using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Author;
using Explorer.Tours.API.Public.Tourist;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Route("api/shopping-cart")]
    public class ShoppingCartController: BaseApiController
    {
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        [HttpGet("create/{touristId:long}")]
        public ActionResult<FacilityDto> Create(long touristId)
        {
            var result = this._shoppingCartService.Create(touristId);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors.FirstOrDefault()?.Message);
            }

            return Ok(result.Value);
        }

        [Authorize(Policy = "touristPolicy")]
        [HttpPost("addItem/{touristId:long}")]
        public ActionResult<FacilityDto> AddItemToCart(OrderItemDto orderItemDto, long touristId)
        {
            var result = this._shoppingCartService.AddToCart(orderItemDto ,touristId);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors.FirstOrDefault()?.Message);
            }

            return Ok(result.Value);
        }

		[Authorize(Policy = "touristPolicy")]
		[HttpDelete("addItem/{touristId:long}")]
        public ActionResult<FacilityDto> RemoveItemToCart(OrderItemDto orderItemDto, long touristId)
        {
			var result = this._shoppingCartService.RemoveFromCart(orderItemDto, touristId);
			if (!result.IsSuccess)
			{
				return BadRequest(result.Errors.FirstOrDefault()?.Message);
			}

			return Ok(result.Value);
		}

		[Authorize(Policy = "touristPolicy")]
        [HttpGet("tourist/{touristId:long}")]
        public ActionResult<FacilityDto> GetByTouristId(long touristId)
        {
            var result = this._shoppingCartService.GetByUserId(touristId);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors.FirstOrDefault()?.Message);
            }

            return Ok(result.Value);
        }
    }
}
