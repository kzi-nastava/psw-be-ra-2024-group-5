using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Tours.API.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
    public class Money : ValueObject
    {
        public double Amount { get; }
        public Currency Currency { get; }
        private Money() { }

        [JsonConstructor]
        public Money(double amount, Currency currency)
        {
            Amount = amount;
            Currency = currency;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Amount;
            yield return Currency;
        }

        public Money Add(Money money)
        {
            //if (!IsSameCurrency(money))
            //    throw new ArgumentException();

            return new Money(money.Amount + Amount, Currency);
        }

        public Money Subtract(Money money)
        {
            if (!IsSameCurrency(money))
                throw new ArgumentException();

            if(money.Amount > Amount)
                throw new ArgumentException();

            return new Money(Amount - money.Amount, Currency);
        }

        public bool IsSameCurrency(Money money)
        {
            if (money.Currency != Currency)
            {
                return false;
            }
            return true;
        }
    }
}
