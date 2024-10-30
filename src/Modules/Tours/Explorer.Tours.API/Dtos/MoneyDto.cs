using Explorer.Tours.API.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class MoneyDto
    {
        public double Amount { get; set; }
        public Currency Currency { get; set; }
        public MoneyDto() { }
        public MoneyDto(double amount, Currency currency)
        {
            Amount = amount;
            Currency = currency;
        }
    }
}
