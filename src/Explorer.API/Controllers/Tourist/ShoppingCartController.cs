using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Dtos.BundleDto;
using Explorer.Payments.API.Public.Tourist;
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
        public ActionResult<ShoppingCartDto> RemoveItemFromCart(OrderItemDto orderItemDto, long touristId)
        {
			var result = this._shoppingCartService.RemoveFromCart(orderItemDto, touristId);
			if (!result.IsSuccess)
			{
				return BadRequest(result.Errors.FirstOrDefault()?.Message);
			}

			return Ok(result.Value);
		}

        [Authorize(Policy = "touristPolicy")]
        [HttpPost("addBundle/{touristId:long}")]
        public ActionResult<ShoppingCartDto> AddBundleToCart(long touristId, [FromBody] long bundleId) {
            var result = this._shoppingCartService.AddBundleToCart(bundleId, touristId);
            if (!result.IsSuccess) {
                return BadRequest(result.Errors.FirstOrDefault()?.Message);
            }

            return Ok(result.Value);
        }

        [Authorize(Policy = "touristPolicy")]
        [HttpDelete("removeBundle/{touristId:long}")]
        public ActionResult<ShoppingCartDto> RemoveBundleFromCart(OrderItemBundleDto orderItemBundleDto, long touristId) {
            var result = this._shoppingCartService.RemoveBundleFromCart(orderItemBundleDto, touristId);
            if (!result.IsSuccess) {
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
		public ActionResult Checkout(long touristId,string code=null)
		{
			var result = _shoppingCartService.Checkout(touristId,code);
			if (!result.IsSuccess)
			{
				return BadRequest(new { error = result.Errors.FirstOrDefault()?.Message });
			}
			return Ok(new { message = "Purchase completed successfully. Tokens created for each item." });
		}
		[Authorize(Policy = "touristPolicy")]
		[HttpGet("items-count/{userId}")]
		public ActionResult<int> GetItemsCount(long userId)
		{
			try
			{
				var count = _shoppingCartService.GetItemsCount(userId);
				return Ok(count);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

	}
}
