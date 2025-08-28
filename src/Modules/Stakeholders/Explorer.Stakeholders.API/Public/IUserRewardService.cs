using Explorer.Stakeholders.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Public
{
    public interface IUserRewardService
    {
        Result<UserRewardDto> GetRewardInfo(long userId);
        Result ClaimDaily(long userId);
        Task<Result<ClaimedRewardDto>> ClaimWheelOfFortune(long userId, int reward);
    }
}
