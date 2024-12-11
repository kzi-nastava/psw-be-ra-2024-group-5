using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Domain.RepositoryInterfaces
{
    public interface IShoppingCartRepository : ICrudRepository<ShoppingCart>
    {
        ShoppingCart GetByUserId(long touristId);
        void SaveToken(TourPurchaseToken token);
        void SavePaymentRecord(PaymentRecord paymentRecord);
        TourPurchaseToken GetPurchaseTokenByTourAndTouristId(long touristId, long tourId);
        bool IsTourBought(long touristId, long tourId);
        bool IsTourInCart(long touristId, long tourId);
        int GetNumberOfPurchasesForTour(long tourId);
    }
}
