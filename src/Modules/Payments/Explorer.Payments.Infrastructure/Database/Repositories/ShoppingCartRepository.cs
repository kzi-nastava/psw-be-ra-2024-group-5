using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Payments.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Infrastructure.Database.Repositories
{
    public class ShoppingCartRepository : CrudDatabaseRepository<ShoppingCart, PaymentsContext>, IShoppingCartRepository
    {
        const bool ITS_BUNDLE = true;
        const bool ITS_TOUR = false;
        public ShoppingCartRepository(PaymentsContext dbContext) : base(dbContext) { }
        public new ShoppingCart Update(ShoppingCart shoppingCart)
        {
            DbContext.Entry(shoppingCart).State = EntityState.Modified;
            DbContext.SaveChanges();
            return shoppingCart;
        }
        public new ShoppingCart Get(long id)
        {
            var shoppingCart = DbContext.ShoppingCarts.Where(sc => sc.Id == id)
                .Include(sc => sc.Items!)
                .FirstOrDefault();

            return shoppingCart;
        }

        public ShoppingCart GetByUserId(long touristId)
        {
            var shoppingCart = DbContext.ShoppingCarts.Where(sc => sc.TouristId == touristId)
                .Include(sc => sc.Items!)
                .FirstOrDefault();

            return shoppingCart;
        }

        public void SaveToken(TourPurchaseToken token)
        {
            DbContext.TourPurchaseTokens.Add(token);
			DbContext.SaveChanges();
		}

        public TourPurchaseToken GetPurchaseTokenByTourAndTouristId(long touristId, long tourId)
        {
            return DbContext.TourPurchaseTokens.Where(tpt => tpt.TourId == tourId && tpt.UserId == touristId).FirstOrDefault();
        }

        public void SavePaymentRecord(PaymentRecord paymentRecord) {
            DbContext.PaymentRecords.Add(paymentRecord);
            DbContext.SaveChanges();
        }

        public bool IsTourBought(long touristId, long tourId)
        {
            var purchaseToken = this.GetPurchaseTokenByTourAndTouristId(touristId, tourId);

            if (purchaseToken == null)
            {
                return false;
            }

            return true;
        }

        public bool IsTourInCart(long touristId, long tourId)
        {
            var shoppingCart = this.GetByUserId(touristId);

            return shoppingCart.ContainsTour(tourId, ITS_TOUR);
        }
        public bool IsBundleInCart(long touristId, long bundleId) {
            var shoppingCart = this.GetByUserId(touristId);

            return shoppingCart.ContainsTour(bundleId, ITS_BUNDLE);
        }
    }
}
