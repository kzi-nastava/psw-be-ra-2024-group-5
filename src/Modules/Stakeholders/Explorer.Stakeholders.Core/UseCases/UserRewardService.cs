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
using Explorer.Payments.API.Public.Author;
using Explorer.Tours.API.Internal;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.API.Dtos;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class UserRewardService : IUserRewardService
    {
        private readonly IUserRepository _userRepository;
        private readonly IInternalWalletService _walletService;
        private readonly IInternalShoppingCartService _shoppingService;
        private readonly IMapper _userMapper;
        private readonly IParticipantService _participantService;
        private readonly ICouponService _couponService;
        private readonly IInternalTourService _tourService;
        public UserRewardService(IUserRepository userRepository, IInternalWalletService walletService, IMapper mapper,
            IParticipantService participantService, 
            ICouponService couponService, 
            IInternalShoppingCartService shoppingService,
            IInternalTourService tourService)
        {
            _userRepository = userRepository;
            _walletService = walletService;
            _userMapper = mapper;
            _participantService = participantService;
            _couponService = couponService;
            _shoppingService = shoppingService;
            _tourService = tourService;
        }

        public Result ClaimDaily(long userId)
        {
            var user = _userRepository.Get(userId);

            if (user == null || user.Role != UserRole.Tourist)
            {
                return Result.Fail(FailureCode.NotFound);
            }

            if (!_participantService.Exists(userId))
                _participantService.Create(new ParticipantDto(userId, 0, 0));

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

        public async Task<Result<ClaimedRewardDto>> ClaimWheelOfFortune(long userId,int reward)
        {
            var user = _userRepository.Get(userId);

            if (user == null || user.Role != UserRole.Tourist)
            {
                return Result.Fail(FailureCode.NotFound);
            }

            ClaimedRewardDto result = new ClaimedRewardDto();

            switch (reward)
            {
                case 1:
                    {
                        if (!_participantService.Exists(userId))
                            _participantService.Create(new ParticipantDto(userId, 0, 0));
                        _participantService.AddXP(userId, 50);
                    }
                    break;

                case 2:
                    {
                        ShoppingMoneyDto shoppingMoneyDto = new ShoppingMoneyDto();
                        shoppingMoneyDto.Amount = 50;
                        shoppingMoneyDto.Currency = Payments.API.Enum.ShoppingCurrency.AC;
                        _walletService.AddFunds(shoppingMoneyDto, userId);
                    }
                    break;
                case 3:
                    {
                        ShoppingMoneyDto shoppingMoneyDto = new ShoppingMoneyDto();
                        shoppingMoneyDto.Amount = 100;
                        shoppingMoneyDto.Currency = Payments.API.Enum.ShoppingCurrency.AC;
                        _walletService.AddFunds(shoppingMoneyDto, userId);
                    }
                    break;
                case 4:
                    {
                        var boughtTours = _shoppingService.GetPurchasedTourIdsByTouristId(userId);
                        var notBought = _tourService.GetTourCardsExceptIds(boughtTours).Value;

                        if (notBought.Any())
                        {
                            var random = new Random();
                            var randomTour = notBought[random.Next(notBought.Count)];

                            var newCoupon = new CouponDto();
                            newCoupon.ExpiredDate = DateTime.Now.AddYears(2);
                            newCoupon.Percentage = 30;
                            newCoupon.TourIds = new List<long> { randomTour.Id };
                            newCoupon.Code = "";
                            var couponResult = await this._couponService.Create(newCoupon);
                            
                            result.TourId = randomTour.Id;
                            result.TourName = randomTour.Name;
                            result.Image = randomTour.FirstKeypoint.Image;
                            result.Code = couponResult.Value.Code;
                            result.Percentage = couponResult.Value.Percentage;
                            result.CouponId = couponResult.Value.Id;
                            result.ExpiredDate = couponResult.Value.ExpiredDate;
                        }
                        else
                        {
                            ShoppingMoneyDto shoppingMoneyDto = new ShoppingMoneyDto();
                            shoppingMoneyDto.Amount = 200;
                            shoppingMoneyDto.Currency = Payments.API.Enum.ShoppingCurrency.AC;
                            _walletService.AddFunds(shoppingMoneyDto, userId);
                        }
                    }
                    break;

                case 5:
                    {
                        var boughtTours = _shoppingService.GetPurchasedTourIdsByTouristId(userId);
                        var notBought = _tourService.GetTourCardsExceptIds(boughtTours).Value;

                        if (notBought.Any())
                        {
                            var random = new Random();
                            var randomTour = notBought[random.Next(notBought.Count)];

                            _shoppingService.CreateToken(user.Id, randomTour.Id);

                            result.TourId = randomTour.Id;
                            result.TourName = randomTour.Name;
                            result.Image = randomTour.FirstKeypoint.Image;
                        }
                        else
                        {
                            ShoppingMoneyDto shoppingMoneyDto = new ShoppingMoneyDto();
                            shoppingMoneyDto.Amount = 200;
                            shoppingMoneyDto.Currency = Payments.API.Enum.ShoppingCurrency.AC;
                            _walletService.AddFunds(shoppingMoneyDto, userId);
                        }
                    }
                    break;
            }

            user.LastRewardClaimed = DateTime.UtcNow;
            _userRepository.Update(user);

            return Result.Ok(result);
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
