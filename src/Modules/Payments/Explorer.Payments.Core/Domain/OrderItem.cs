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
        public long TourId { get; private set; }
        public string TourName { get; private set; }
        public Money Price { get; private set; }
        public OrderItem() { }
        public OrderItem(long tourId, string tourName, Money price)
        {
            TourId = tourId;
            TourName = tourName;
            Price = price;
        }
    }
}
