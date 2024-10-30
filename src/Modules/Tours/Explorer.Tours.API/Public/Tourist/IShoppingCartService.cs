using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public.Tourist
{
    public interface IShoppingCartService
    {
        Result<ShoppingCartDto> Create(long userId);
        Result<ShoppingCartDto> AddToCart(OrderItemDto orderItemDto, long touristId);
    }
}
