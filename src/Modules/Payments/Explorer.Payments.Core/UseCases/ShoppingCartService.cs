using Explorer.Stakeholders.API.Internal;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public.Tourist;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using FluentResults;
using Explorer.Tours.API.Internal;
using Explorer.Payments.API.Dtos.BundleDto;
using System.Reflection.Metadata;

namespace Explorer.Tours.Core.UseCases.Tourist
{
    public class ShoppingCartService: IShoppingCartService
    {
        const bool ITS_TOUR = false;
        const bool ITS_BUNDLE = true;

        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IInternalTourService _tourService;
        private readonly IUserService _userService;
        private readonly IWalletService _walletService;
        private readonly IBundleRepository _bundleRepository;
        private readonly ICouponRepository _couponRepository;
        public ShoppingCartService(IShoppingCartRepository shoppingCartRepository, IUserService userService, IInternalTourService tourService, IWalletService walletService, IBundleRepository bundleRepository, ICouponRepository couponRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _userService = userService;
            _tourService = tourService;
            _walletService = walletService;
            _bundleRepository = bundleRepository;
            _couponRepository = couponRepository;
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

                foreach (var item in shoppingCart.Items) {
                    if (item.IsBundle) {
                        var otherBundle = _bundleRepository.Get(item.ItemId);
                        if (otherBundle.BundleItems.Any(x => x == tour.Value.Id)) {
                            return Result.Fail("Tura se vec nalazi u nekom bundle-u, zamenite stari bundle sa novom turom ili ostavite bundle!"); //Ovo kasnije izmeni tako da izadje ponudjeno da li hoce novi bundle ili stari
                        }
                    }
                }
                var price = new Money(orderItemDto.Price.Amount, orderItemDto.Price.Currency);
                var orderItem = new OrderItem(orderItemDto.TourId, orderItemDto.TourName, price, ITS_TOUR);

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
                    item.IsBundle == ITS_TOUR &&
                    item.ItemId == orderItemDto.TourId &&
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

        public Result<ShoppingCartDto> AddBundleToCart(long bundleId, long touristId) {
            try {
                var shoppingCart = _shoppingCartRepository.GetByUserId(touristId);

                if (shoppingCart == null)
                    return Result.Fail("Shopping cart doesnt exist!");

                var bundle = _bundleRepository.Get(bundleId);

                if (bundle == null)
                    return Result.Fail("Bundle doesnt exist!");

                foreach(var item in shoppingCart.Items) {
                    if (item.IsBundle) {
                        var otherBundle = _bundleRepository.Get(item.ItemId);
                        if(otherBundle.BundleItems.Any(x => bundle.BundleItems.Contains(x))) {
                            return Result.Fail("Neka od tura se vec nalazi u nekom bundle-u, zamenite stari bundle sa novim ili ostavite stari!"); //Ovo kasnije izmeni tako da izadje ponudjeno da li hoce novi bundle ili stari
                        }
                    }
                }

                var price = new Money(bundle.Price.Amount, bundle.Price.Currency);
                var orderItemBundle = new OrderItem(bundle.Id, bundle.Name, price, ITS_BUNDLE); 

                shoppingCart.AddBundleToCart(orderItemBundle, bundle);
                shoppingCart = _shoppingCartRepository.Update(shoppingCart);

                return MapShoppingCartToDto(shoppingCart);
            }
            catch (Exception ex) {
                return Result.Fail(ex.Message);
            }
        }
        public Result<ShoppingCartDto> RemoveBundleFromCart(OrderItemBundleDto orderItemBundleDto, long touristId) {
            try {
                var shoppingCart = _shoppingCartRepository.GetByUserId(touristId);

                if (shoppingCart == null)
                    return Result.Fail("Shopping cart doesnt exist!");

                var orderItem = shoppingCart.Items.FirstOrDefault(item =>
                    item.IsBundle == ITS_BUNDLE &&
                    item.ItemId == orderItemBundleDto.BundleId &&
                    item.Price.Amount == orderItemBundleDto.Price.Amount &&
                    item.Price.Currency == orderItemBundleDto.Price.Currency);

                //moze i po ID-u ?

                if (orderItem == null)
                    return Result.Fail("Item does not exist in the shopping cart!");

                shoppingCart.RemoveItemFromCart(orderItem);
                shoppingCart = _shoppingCartRepository.Update(shoppingCart);

                return MapShoppingCartToDto(shoppingCart);
            }
            catch (Exception ex) {
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
            var orderItemBundles = new List<OrderItemBundleDto>();

            foreach (var order in shoppingCart.Items)
            {
                var price = new ShoppingMoneyDto(order.Price.Amount, order.Price.Currency);

                if (order.IsBundle) {
                    var bundle = _bundleRepository.Get(order.ItemId);

                    if (bundle == null) {
                        throw new Exception($"Bundle with ID {order.ItemId} not found");
                    }

                    orderItemBundles.Add(new OrderItemBundleDto(order.Id, order.ItemId, order.ItemName, price));

                }
                else {
				    var tour = _tourService.GetById(order.ItemId);

				    if (tour == null)
				    {
					    throw new Exception($"Tour with ID {order.ItemId} not found");
				    }

				    orderItems.Add(new OrderItemDto(order.Id, order.ItemId, order.ItemName, price, tour.Value.Description, tour.Value.Tags,tour.Value.ArchivedTime,tour.Value.PublishedTime));
                }

            }
            var result = new ShoppingCartDto(shoppingCart.Id, orderItems, orderItemBundles, shoppingCart.TouristId, totalPrice);

            return result;
        }

		public Result Checkout(long touristId, string code)
		{
			var shoppingCart = _shoppingCartRepository.GetByUserId(touristId);
			if (shoppingCart == null || !shoppingCart.Items.Any())
			{
				return Result.Fail("Shopping cart is empty.");
			}

            var coupon = _couponRepository.GetByCode(code);

			double discountPercentage = 0;

			if (coupon != null)
            {
				discountPercentage = coupon.Percentage/100.0; //od 10% je 0.1

			}

			var originalTotalPrice = shoppingCart.TotalPrice.Amount;
			double discountedTotalPrice = originalTotalPrice;

			if (coupon != null)
			{
				if (coupon.TourIds != null && coupon.TourIds.Any())
				{
					foreach (var item in shoppingCart.Items)
					{
						if (coupon.TourIds.Contains(item.ItemId)) 
						{
							discountedTotalPrice -= item.Price.Amount * discountPercentage;
						}
					}
				}
				else
				{
					var mostExpensiveItem = shoppingCart.Items.OrderByDescending(i => i.Price.Amount).FirstOrDefault();
					if (mostExpensiveItem != null)
					{
						discountedTotalPrice -= mostExpensiveItem.Price.Amount * discountPercentage;
					}
				}
			}

			var totalPrice = new ShoppingMoneyDto(discountedTotalPrice, shoppingCart.TotalPrice.Currency);


			var result = _walletService.AreEnoughFundsInWallet(totalPrice, touristId);

            if (!result.IsSuccess || !result.Value)
            {
                return Result.Fail("Not enough funds in wallet.");
            }

            var removeFundsResult = _walletService.RemoveFunds(totalPrice, touristId);

            if (!result.IsSuccess)
            {
                return Result.Fail(result.Errors);
            }

            //var tokens = new List<TourPurchaseToken>();

			foreach (var order in shoppingCart.Items)
			{
                if (order.IsBundle) {
                    var bundle = _bundleRepository.Get(order.ItemId);
                    if (bundle == null)
                        return Result.Fail("Error while getting bundles");

                    foreach (var tId in bundle.BundleItems) {
                        var token = new TourPurchaseToken {
                            TourId = tId,              
                            UserId = touristId,
                            PurchaseDate = DateTime.UtcNow
                        };
                        //tokens.Add(token);
                        _shoppingCartRepository.SaveToken(token);
                    }
                }
                else {
                    var token = new TourPurchaseToken
				    {
					    TourId = order.ItemId,               
					    UserId = touristId,
					    PurchaseDate = DateTime.UtcNow
				    };
				    //tokens.Add(token);
				    _shoppingCartRepository.SaveToken(token);
                }
                _shoppingCartRepository.SavePaymentRecord(new PaymentRecord(touristId, order.ItemId, order.Price, order.IsBundle));
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
