using Explorer.Stakeholders.API.Internal;
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
        private readonly ITourRepository _tourRepository;
        private readonly IUserService _userService;
        public ShoppingCartService(IShoppingCartRepository shoppingCartRepository, IUserService userService, ITourRepository tourRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _userService = userService;
            _tourRepository = tourRepository;
        }

        public Result<ShoppingCartDto> Create(long touristId)
        {
            try
            {
                if (!_userService.CheckTouristExists(touristId))
                    return Result.Fail("Tourist doesnt exist!");

                var existingCart = _shoppingCartRepository.GetByUserId(touristId);

                if (existingCart != null)
                    return Result.Fail("Shopping cart already exists for that tourist!");

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

                if (shoppingCart == null)
                   return Result.Fail("Shopping cart doesnt exist!");

                var tour = _tourRepository.Get(orderItemDto.TourId);

                if (tour == null)
                    return Result.Fail("Tour doesnt exist!");

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
		public Result<ShoppingCartDto> RemoveFromCart(OrderItemDto orderItemDto, long touristId)
        {
			try
			{
				var shoppingCart = _shoppingCartRepository.GetByUserId(touristId);

				if (shoppingCart == null)
					return Result.Fail("Shopping cart doesnt exist!");

				var orderItem = shoppingCart.Items.FirstOrDefault(item =>
			        item.TourId == orderItemDto.TourId &&
			        item.Price.Amount == orderItemDto.Price.Amount &&
			        item.Price.Currency == orderItemDto.Price.Currency);

				if (orderItem == null)
					return Result.Fail("Item does not exist in the shopping cart!");

				shoppingCart.RemoveItemFromCart(orderItem);
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

		public Result Checkout(long touristId)
		{
			var shoppingCart = _shoppingCartRepository.GetByUserId(touristId);
			if (shoppingCart == null || !shoppingCart.Items.Any())
			{
				return Result.Fail("Shopping cart is empty.");
			}

			var tokens = new List<TourPurchaseToken>();

			foreach (var item in shoppingCart.Items)
			{
				var token = new TourPurchaseToken
				{
					TourId = item.TourId,
					UserId = touristId,
					PurchaseDate = DateTime.UtcNow
				};
				tokens.Add(token);
				_shoppingCartRepository.SaveToken(token);
			}

			shoppingCart.Items.Clear();
			shoppingCart.ResetTotalPrice(); 
			_shoppingCartRepository.Update(shoppingCart);

			return Result.Ok();
		}
	}
}
