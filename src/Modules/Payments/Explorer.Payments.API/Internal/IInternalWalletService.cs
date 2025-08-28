using Explorer.Payments.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Internal
{
    public interface IInternalWalletService
    {
        Result CreateWallet(long touristId);
        Result<WalletDto> AddFunds(ShoppingMoneyDto moneyDto, long touristId);
    }
}
