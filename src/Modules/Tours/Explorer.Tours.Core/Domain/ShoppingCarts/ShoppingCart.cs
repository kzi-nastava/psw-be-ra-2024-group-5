using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Tours.API.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.ShoppingCarts
{
    public class ShoppingCart: Entity
    {
        public Money TotalPrice { get; private set; }
        public long TouristId { get; private set; }
        public List<OrderItem> Items { get; init; }
        public ShoppingCart() { }
        public ShoppingCart(long userId)
        {
            TouristId = userId;
            Items = new List<OrderItem>();
            TotalPrice = new Money(0, Currency.Rsd);
        }
    }
}
