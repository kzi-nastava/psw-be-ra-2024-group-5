using AutoMapper;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Internal;
using Explorer.Payments.API.Public.Tourist;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.Domain;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Stakeholders.API.Public;

namespace Explorer.Payments.Core.UseCases
{
    public class WalletService : IWalletService, IInternalWalletService
    {
        private readonly IWalletRepository _walletRepository;
        private readonly INotificationService _notificationService;

        private readonly IMapper _mapper;
        public WalletService(IWalletRepository walletRepository, IMapper mapper, INotificationService notificationService) {
            _walletRepository = walletRepository;
            _mapper = mapper;
            _notificationService = notificationService;
        }
        public Result CreateWallet(long touristId)
        {
            try
            {
                _walletRepository.Create(new Wallet(touristId));
                return Result.Ok();
            }
            catch (Exception e)
            {
                return Result.Fail(e.Message);
            }
        }

        public Result<WalletDto> AddFunds(ShoppingMoneyDto moneyDto, long touristId)
        {
            try
            {
                var wallet = _walletRepository.GetByTouristId(touristId);

                if (wallet == null)
                {
                    return Result.Fail("This user doesnt have a wallet");
                }

                var money = _mapper.Map<Money>(moneyDto);
                var moneyAC = money.ConvertToAC(money);

                wallet.AddFunds(moneyAC);
                wallet = _walletRepository.Update(wallet);

                SendNotificationForWalletUpdate(touristId);

                var walletDto = _mapper.Map<WalletDto>(wallet);

                return Result.Ok(walletDto);
            }
            catch (Exception e)
            {
                return Result.Fail(e.Message);
            }
        }

        public Result<WalletDto> GetByTouristId(long touristId)
        {
            try
            {
                var wallet = _walletRepository.GetByTouristId(touristId);

                if (wallet == null)
                {
                    return Result.Fail("This user doesnt have a wallet");
                }

                return Result.Ok(_mapper.Map<WalletDto>(wallet));
            }
            catch (Exception e)
            {
                return Result.Fail(e.Message);
            }
        }

        public Result<bool> AreEnoughFundsInWallet(ShoppingMoneyDto moneyDto, long touristId)
        {
            try
            {
                var wallet = _walletRepository.GetByTouristId(touristId);

                if (wallet == null)
                {
                    return Result.Fail("This user doesnt have a wallet");
                }

                var money = _mapper.Map<Money>(moneyDto);

                return Result.Ok(wallet.IsEnoughFunds(money));
            }
            catch (Exception e)
            {
                return Result.Fail(e.Message);
            }
        }

        public Result<WalletDto> RemoveFunds(ShoppingMoneyDto moneyDto, long touristId)
        {
            try
            {
                var wallet = _walletRepository.GetByTouristId(touristId);

                if (wallet == null)
                {
                    return Result.Fail("This user doesnt have a wallet");
                }

                var money = _mapper.Map<Money>(moneyDto);

                wallet.RemoveFunds(money.ConvertToAC(money));
                wallet = _walletRepository.Update(wallet);

                var walletDto = _mapper.Map<WalletDto>(wallet);

                return Result.Ok(walletDto);
            }
            catch (Exception e)
            {
                return Result.Fail(e.Message);
            }
        }


        private void SendNotificationForWalletUpdate(long touristId)
        {
            var userIds = new List<long> { touristId };

            var notificationDto = new NotificationDto
            {
                Content = "Funds to your wallet have been added! " +
                "Checkout our new deals, and embark on a new adventure!",
                CreatedAt = DateTime.UtcNow,
                UserIds = userIds,
                Type = 5,
                UserReadStatuses = userIds.Select(userId => new NotificationReadStatusDto
                {
                    UserId = userId,
                    NotificationId = 0,
                    IsRead = false
                }).ToList()
            };

            _notificationService.SendNotification(notificationDto);
        }
    }
}
