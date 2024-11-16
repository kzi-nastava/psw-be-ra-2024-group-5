using Explorer.Payments.API.Internal;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
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
        public InternalShoppingCartService(IShoppingCartRepository shoppingCartRepository) {
            _shoppingCartRepository = shoppingCartRepository;
        }
        public bool IsTourBought(long touristId, long tourId)
        {
            return _shoppingCartRepository.IsTourBought(touristId, tourId);
        }

        public bool IsTourInCart(long touristId, long tourId)
        {
            return _shoppingCartRepository.IsTourInCart(touristId, tourId);
        }
    }
}
