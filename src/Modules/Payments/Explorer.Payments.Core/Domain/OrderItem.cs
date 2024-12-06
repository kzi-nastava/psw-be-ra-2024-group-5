using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Domain
{
    public class OrderItem: Entity
    {
        public long ItemId { get; private set; }
        public string ItemName { get; private set; }
        public Money Price { get; private set; }
        public bool IsBundle { get; private set; }
        public OrderItem() { }
        public OrderItem(long itemId, string itemName, Money price, bool isBundle)
        {
            ItemId = itemId;
            ItemName = itemName;
            Price = price;
            IsBundle = isBundle;
        }
    }
}
