using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Internal;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using AutoMapper;
using Explorer.Payments.API.Dtos;
using Explorer.Encounters.API.Internal;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class UserRewardService : IUserRewardService
    {
        private readonly IUserRepository _userRepository;
        private readonly IInternalWalletService _walletService;
        private readonly IMapper _userMapper;
        private readonly IInternalParticipantService _participantService;
        public UserRewardService(IUserRepository userRepository, IInternalWalletService walletService, IMapper mapper, IInternalParticipantService participantService)
        {
            _userRepository = userRepository;
            _walletService = walletService;
            _userMapper = mapper;
            _participantService = participantService;
        }

        public Result ClaimDaily(long userId)
        {
            var user = _userRepository.Get(userId);

            if (user == null || user.Role != UserRole.Tourist)
            {
                return Result.Fail(FailureCode.NotFound);
            }

            switch (user.RewardStreak)
            {
                case 0:
                    {
                        _participantService.AddXP(userId, 50);
                    }
                break;

                case 1:
                    {
                        ShoppingMoneyDto shoppingMoneyDto = new ShoppingMoneyDto();
                        shoppingMoneyDto.Amount = 50;
                        shoppingMoneyDto.Currency = Payments.API.Enum.ShoppingCurrency.AC;
                        _walletService.AddFunds(shoppingMoneyDto, userId);
                    }
                break;
                case 3:
                    {
                        _participantService.AddXP(userId, 50);
                    }
                    break;
                case 4:
                    {
                        ShoppingMoneyDto shoppingMoneyDto = new ShoppingMoneyDto();
                        shoppingMoneyDto.Amount = 100;
                        shoppingMoneyDto.Currency = Payments.API.Enum.ShoppingCurrency.AC;
                        _walletService.AddFunds(shoppingMoneyDto, userId);
                    }
                    break;

                case 5:
                    {
                        ShoppingMoneyDto shoppingMoneyDto = new ShoppingMoneyDto();
                        shoppingMoneyDto.Amount = 200;
                        shoppingMoneyDto.Currency = Payments.API.Enum.ShoppingCurrency.AC;
                        _walletService.AddFunds(shoppingMoneyDto, userId);
                    }
                    break;
            }

            user.LastRewardClaimed = DateTime.UtcNow;
            _userRepository.Update(user);

            return Result.Ok();
        }

        public Result ClaimWheelOfFortune(long userId, uint reward)
        {
            throw new NotImplementedException();
        }

        public Result<UserRewardDto> GetRewardInfo(long userId)
        {
            var user = _userRepository.Get(userId);

            if (user == null || user.Role != UserRole.Tourist)
            {
                return Result.Fail(FailureCode.NotFound);
            }

            var rewardDto = _userMapper.Map<UserRewardDto>(user);

            if (user.LastRewardClaimed.Date < DateTime.Today)
            {
                rewardDto.CanBeClaimed = true;
            }
            else
            {
                rewardDto.CanBeClaimed = false;
            }

            return rewardDto;
        }
    }
}
