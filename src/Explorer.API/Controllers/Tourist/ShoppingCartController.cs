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
        public ActionResult<ShoppingCartDto> AddItemToCart(OrderItemDto orderItemDto, long touristId)
        {
            var result = this._shoppingCartService.AddToCart(orderItemDto ,touristId);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors.FirstOrDefault()?.Message);
            }

            return Ok(result.Value);
        }

		[Authorize(Policy = "touristPolicy")]
		[HttpDelete("removeItem/{touristId:long}")]
        public ActionResult<ShoppingCartDto> RemoveItemToCart(OrderItemDto orderItemDto, long touristId)
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
        public ActionResult<ShoppingCartDto> GetByTouristId(long touristId)
        {
            var result = this._shoppingCartService.GetByUserId(touristId);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors.FirstOrDefault()?.Message);
            }

            return Ok(result.Value);
        }

		[Authorize(Policy = "touristPolicy")]
		[HttpPost("checkout/{touristId:long}")]
		public ActionResult Checkout(long touristId)
		{
			var result = _shoppingCartService.Checkout(touristId);
			if (!result.IsSuccess)
			{
				return BadRequest(result.Errors.FirstOrDefault()?.Message);
			}

			return Ok("Purchase completed successfully. Tokens created for each item.");
		}
	}
}
