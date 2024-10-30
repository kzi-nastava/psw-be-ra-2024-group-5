using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Tourist;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.ShoppingCarts;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.Tourist
{
    public class ShoppingCartService: IShoppingCartService
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        public ShoppingCartService(IShoppingCartRepository shoppingCartRepository) {
            _shoppingCartRepository = shoppingCartRepository;
        }

        public Result<ShoppingCartDto> Create(long touristId)
        {
            try
            {
                var shoppingCart = new ShoppingCart(touristId);
                
                shoppingCart = _shoppingCartRepository.Create(shoppingCart);

                return MapShoppingCartToDto(shoppingCart);
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        public Result<ShoppingCartDto> AddToCart(OrderItemDto orderItemDto, long touristId)
        {
            try
            {
                var shoppingCart = _shoppingCartRepository.GetByUserId(touristId);

                var price = new Money(orderItemDto.Price.Amount, orderItemDto.Price.Currency);
                var orderItem = new OrderItem(orderItemDto.TourId, orderItemDto.TourName, price);

                shoppingCart.AddItemToCart(orderItem);
                shoppingCart = _shoppingCartRepository.Update(shoppingCart);

                return MapShoppingCartToDto(shoppingCart);
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        public Result<ShoppingCartDto> GetByUserId(long touristId)
        {
            try
            {
                var shoppingCart = _shoppingCartRepository.GetByUserId(touristId);

                return MapShoppingCartToDto(shoppingCart);
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }


        private ShoppingCartDto MapShoppingCartToDto(ShoppingCart shoppingCart)
        {
            var totalPrice = new MoneyDto(shoppingCart.TotalPrice.Amount, shoppingCart.TotalPrice.Currency);

            var orderItems = new List<OrderItemDto>();

            foreach (var order in shoppingCart.Items)
            {
                var price = new MoneyDto(order.Price.Amount, order.Price.Currency);
                orderItems.Add(new OrderItemDto(order.Id, order.TourId, order.TourName, price));

            }

            var result = new ShoppingCartDto(shoppingCart.Id, orderItems, shoppingCart.TouristId, totalPrice);

            return result;
        }
    }
}
