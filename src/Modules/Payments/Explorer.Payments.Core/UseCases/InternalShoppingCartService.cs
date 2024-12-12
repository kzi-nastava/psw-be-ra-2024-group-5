using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Internal;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.API.Internal;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.UseCases
{
    public class InternalShoppingCartService: IInternalShoppingCartService
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IUserService _userService;
        public InternalShoppingCartService(IShoppingCartRepository shoppingCartRepository, IUserService userService)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _userService = userService;
        }
        public bool IsTourBought(long touristId, long tourId)
        {
            return _shoppingCartRepository.IsTourBought(touristId, tourId);
        }

        public bool IsTourInCart(long touristId, long tourId)
        {
            return _shoppingCartRepository.IsTourInCart(touristId, tourId);
        }


        public Result Create(long touristId)
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

                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        public int GetNumberOfPurchasesForTour(long tourId)
        {
            return _shoppingCartRepository.GetNumberOfPurchasesForTour(tourId);
        }
    }
}
