using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.ShoppingCarts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class ShoppingCartRepository : CrudDatabaseRepository<ShoppingCart, ToursContext>, IShoppingCartRepository
    {
        public ShoppingCartRepository(ToursContext dbContext) : base(dbContext) { }
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

            return shoppingCart.ContainsTour(tourId);
        }
    }
}
