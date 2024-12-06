using Explorer.Payments.API.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Dtos
{
    public class ShoppingMoneyDto
    {
        public double Amount { get; set; }
        public ShoppingCurrency Currency { get; set; }
        public ShoppingMoneyDto() { }
        public ShoppingMoneyDto(double amount, ShoppingCurrency currency)
        {
            Amount = amount;
            Currency = currency;
        }
    }
}
