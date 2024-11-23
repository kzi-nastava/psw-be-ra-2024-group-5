using Explorer.Payments.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Public.Tourist
{
    public interface IWalletService
    {
        Result<WalletDto> AddFunds(ShoppingMoneyDto moneyDto, long touristId);
        Result<WalletDto> GetByTouristId(long touristId);
        Result<bool> AreEnoughFundsInWallet(ShoppingMoneyDto moneyDto, long touristId);
        Result<WalletDto> RemoveFunds(ShoppingMoneyDto moneyDto, long touristId);
    }
}
