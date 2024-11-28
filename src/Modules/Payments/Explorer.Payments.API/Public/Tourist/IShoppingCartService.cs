using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Dtos.BundleDto;
using FluentResults;

namespace Explorer.Payments.API.Public.Tourist
{
    public interface IShoppingCartService
    {
        Result<ShoppingCartDto> AddToCart(OrderItemDto orderItemDto, long touristId);
        Result<ShoppingCartDto> RemoveFromCart(OrderItemDto orderItemDto, long touristId);
        Result<ShoppingCartDto> AddBundleToCart(long bundleId, long touristId);
        Result<ShoppingCartDto> RemoveBundleFromCart(OrderItemBundleDto orderItemBundleDto, long touristId);
        Result<ShoppingCartDto> GetByUserId(long touristId);
		Result Checkout(long touristId);
        public int GetItemsCount(long userId);

	}
}
