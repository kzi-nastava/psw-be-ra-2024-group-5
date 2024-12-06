using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Payments.API.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Domain
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
            TotalPrice = new Money(0, ShoppingCurrency.Rsd);
        }

        public void AddItemToCart(OrderItem orderItem)
        {
            if (this.ContainsTour(orderItem.ItemId, orderItem.IsBundle))
                throw new Exception("Items list already contains an item with the same TourId");

            TotalPrice = TotalPrice.Add(orderItem.Price);
            Items.Add(orderItem);
        }
        public void AddBundleToCart(OrderItem orderItem, Bundle bundle) { // Ako postoje ture u bundle koje su vec u shopping cart-u onda se zbog povoljnije cene one brisu iz shopinga karta a ostaje ceo bundle
            if (this.ContainsTour(orderItem.ItemId, orderItem.IsBundle))
                throw new Exception("Items list already contains an item with the same TourId");

            List<OrderItem> itemsToRemove = new List<OrderItem>();

            foreach (var item in Items) {
                if(item.IsBundle == false && bundle.BundleItems.Contains(item.ItemId)) {
                    itemsToRemove.Add(item);
                }
            }
            foreach (var item in itemsToRemove) {
                RemoveItemFromCart(item);
            }

            AddItemToCart(orderItem);
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

        public bool ContainsTour(long itemId, bool isBundle)
        {
            if (Items.Any(item => item.IsBundle == isBundle && item.ItemId == itemId))
                return true;

            return false;
        }
        public bool ContainsBundleTours(Bundle bundle) {
            if (bundle.BundleItems.Any(x => this.ContainsTour(x, false)))
                return true;

            return false;
        }
    }
}
