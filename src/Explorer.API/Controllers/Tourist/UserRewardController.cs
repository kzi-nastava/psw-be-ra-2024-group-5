using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Numerics;

namespace Explorer.API.Controllers.Tourist
{

    [Route("api/reward")]
    public class UserRewardController: BaseApiController
    {
        private readonly IUserRewardService _userRewardService;
        public UserRewardController(IUserRewardService userRewardService)
        {
            _userRewardService = userRewardService;
        }

        [Authorize(Policy = "touristPolicy")]
        [HttpGet("{userId:long}")]
        public ActionResult<UserRewardDto> GetByUserId(long userId)
        {
            var result = this._userRewardService.GetRewardInfo(userId);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors.FirstOrDefault()?.Message);
            }

            return Ok(result.Value);
        }

        [Authorize(Policy = "touristPolicy")]
        [HttpPost("daily/{userId:long}")]
        public ActionResult ClaimDaily(long userId)
        {
            var result = this._userRewardService.ClaimDaily(userId);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors.FirstOrDefault()?.Message);
            }

            return Ok();
        }

        [Authorize(Policy = "touristPolicy")]
        [HttpPost("wheel/{userId:long}/{rewardType:int}")]
        public async Task<ActionResult<ClaimedRewardDto>> ClaimWheelOfFortune(long userId, int rewardType)
        {
            var result = await this._userRewardService.ClaimWheelOfFortune(userId,rewardType);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors.FirstOrDefault()?.Message);
            }

            return Ok(result.Value);
        }
    }
}
