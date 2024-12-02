using Explorer.Payments.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Public.Tourist
{
    public interface IShoppingCartService
    {
        Result<ShoppingCartDto> AddToCart(OrderItemDto orderItemDto, long touristId);
        Result<ShoppingCartDto> RemoveFromCart(OrderItemDto orderItemDto, long touristId);
        Result<ShoppingCartDto> GetByUserId(long touristId);
		Result Checkout(long touristId,string code);
        public int GetItemsCount(long userId);

	}
}
