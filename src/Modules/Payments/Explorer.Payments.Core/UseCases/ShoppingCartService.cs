using Explorer.Stakeholders.API.Internal;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public.Tourist;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Tours.API.Internal;
using Explorer.Payments.API.Internal;

namespace Explorer.Tours.Core.UseCases.Tourist
{
    public class ShoppingCartService: IShoppingCartService
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IInternalTourService _tourService;
        private readonly IUserService _userService;
        public ShoppingCartService(IShoppingCartRepository shoppingCartRepository, IUserService userService, IInternalTourService tourService)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _userService = userService;
            _tourService = tourService;
        }

        public Result<ShoppingCartDto> AddToCart(OrderItemDto orderItemDto, long touristId)
        {
            try
            {
                var shoppingCart = _shoppingCartRepository.GetByUserId(touristId);

                if (shoppingCart == null)
                   return Result.Fail("Shopping cart doesnt exist!");

                var tour = _tourService.GetById(orderItemDto.TourId);

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
            var totalPrice = new ShoppingMoneyDto(shoppingCart.TotalPrice.Amount, shoppingCart.TotalPrice.Currency);

            var orderItems = new List<OrderItemDto>();

            foreach (var order in shoppingCart.Items)
            {
                var price = new ShoppingMoneyDto(order.Price.Amount, order.Price.Currency);

				var tour = _tourService.GetById(order.TourId);

				if (tour == null)
				{
					throw new Exception($"Tour with ID {order.TourId} not found");
				}

				orderItems.Add(new OrderItemDto(order.Id, order.TourId, order.TourName, price, tour.Value.Description, tour.Value.Tags,tour.Value.ArchivedTime,tour.Value.PublishedTime));

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

		public int GetItemsCount(long userId)
		{
			try
			{
				var shoppingCart = _shoppingCartRepository.GetByUserId(userId);
				return shoppingCart?.Items?.Count ?? 0;  // Ako nema korpe ili stavki, vraća 0
			}
			catch (Exception ex)
			{
				throw new Exception("Error retrieving items count", ex);
			}
		}

	}
}
