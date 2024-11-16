using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Dtos
{
    public class OrderItemDto
    {
        public long? Id { get; set; }
        public long TourId { get; set; }
        public string TourName { get; set; }
        public ShoppingMoneyDto Price { get; set; }
        public OrderItemDto() { }   
        public OrderItemDto(long id, long tourId, string tourName, ShoppingMoneyDto price)
        {
            Id = id;
            TourId = tourId;
            TourName = tourName;
            Price = price;
        }
    }
}
