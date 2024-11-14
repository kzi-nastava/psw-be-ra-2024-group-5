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

        public void AddItemToCart(OrderItem orderItem)
        {
            if (this.ContainsTour(orderItem.TourId))
                throw new Exception("Items list already contains an item with the same TourId");

            TotalPrice = TotalPrice.Add(orderItem.Price);
            Items.Add(orderItem);
        }
		public void RemoveItemFromCart(OrderItem orderItem)
		{
			if (!Items.Contains(orderItem))
				throw new Exception("Items list does not contain that item");

			TotalPrice = TotalPrice.Subtract(orderItem.Price);
			Items.Remove(orderItem);
		}
		public void ResetTotalPrice()
		{
			TotalPrice = new Money(0, TotalPrice.Currency); 
		}

        public bool ContainsTour(long tourId)
        {
            if (Items.Any(item => item.TourId == tourId))
                return true;

            return false;
        }
	}
}
