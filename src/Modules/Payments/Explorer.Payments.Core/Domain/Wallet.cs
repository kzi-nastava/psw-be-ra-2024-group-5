using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Domain
{
    public class Wallet : Entity
    {
        public long TouristId { get; private set; }
        public Money Balance { get; private set; }

        public Wallet(){}

        public Wallet(long userId)
        {
            TouristId = userId;
            Balance = new Money(0.0, API.Enum.ShoppingCurrency.AC);
        }

        public void AddFunds(Money money)
        {
            Balance = Balance.Add(money);
        }

        public Money RemoveFunds(Money money)
        {
            if (!IsEnoughFunds(money))
            {
                throw new ArgumentException("Not enough funds!");
            }

            Balance = Balance.Subtract(money);
            return Balance;
        }

        public bool IsEnoughFunds(Money money)
        {
            if (money.Amount >= Balance.Amount)
            {
                return false;
            }

            return true;
        }
    }
}
