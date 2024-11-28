using Explorer.Payments.API.Dtos.BundleDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Dtos
{
    public class ShoppingCartDto
    {
        public long? Id { get; set; }
        public List<OrderItemDto> Items { get; set; }
        public List<OrderItemBundleDto> Bundles { get; set; }
        public long UserId { get; set; }
        public ShoppingMoneyDto TotalPrice { get; set; }
        public ShoppingCartDto() { }

        public ShoppingCartDto(long id, List<OrderItemDto> items, List<OrderItemBundleDto> bundles, long userId, ShoppingMoneyDto totalPrice)
        {
            Id = id;
            Items = items;
            Bundles = bundles;
            UserId = userId;
            TotalPrice = totalPrice;
        }
    }
}
