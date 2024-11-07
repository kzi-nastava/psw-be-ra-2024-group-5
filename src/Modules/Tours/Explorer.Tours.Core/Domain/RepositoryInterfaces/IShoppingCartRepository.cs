using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.Core.Domain.ShoppingCarts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces
{
    public interface IShoppingCartRepository : ICrudRepository<ShoppingCart>
    {
        ShoppingCart GetByUserId(long touristId);
        void SaveToken(TourPurchaseToken token);

	}
}
