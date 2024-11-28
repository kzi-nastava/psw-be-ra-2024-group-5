using System;
using System.Collections.Generic;
using System.Data;
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
		public string Description { get; set; }
		public string Tags { get; set; }
		public DateTime PublishedTime { get;  set; }
		public DateTime ArchivedTime { get;  set; }
		public OrderItemDto() { }   
        public OrderItemDto(long id, long tourId, string tourName, ShoppingMoneyDto price, string description, string tags, DateTime publishedTime, DateTime archivedTime)
        {
            Id = id;
            TourId = tourId;
            TourName = tourName;
            Price = price;
            Description = description;
            Tags = tags;
            PublishedTime = publishedTime;
            ArchivedTime = archivedTime;
        }
    }
}
