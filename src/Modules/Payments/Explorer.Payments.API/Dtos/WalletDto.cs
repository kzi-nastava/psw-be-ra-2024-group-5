using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Dtos
{
    public class WalletDto
    {
        public long Id { get; set; }
        public ShoppingMoneyDto Balance { get; set; }
        public long TouristId { get; set; }
        public WalletDto(long id, ShoppingMoneyDto balance, long touristId)
        {
            Id = id;
            Balance = balance;
            TouristId = touristId;
        }
        public WalletDto() { }
    }
}
