using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Tours.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers
{
    [Route("api/administration/profile")]
    public class UserProfileController : BaseApiController
    {
        private readonly IUserProfileService _userProfileService;

        public UserProfileController(IUserProfileService userProfileService)
        {
            _userProfileService = userProfileService;
        }
        [HttpPost]
        public ActionResult<UserProfileDto> Create([FromBody] UserProfileDto userProfile)
        {
            var result = _userProfileService.Create(userProfile);
            return CreateResponse(result);
        }

        [HttpGet("{id:int}")]
        public ActionResult<UserProfileDto> GetProfile(long id)
        {
            var result = _userProfileService.Get(id);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        [Authorize]
        public ActionResult<UserProfileDto> UpdateProfile(int id, [FromBody] UserProfileDto userProfile)
        {
            var currentUserId = GetCurrentUserId();  
            if (userProfile.UserId != currentUserId)
            {
                return Forbid();  
            }

            userProfile.Id = id;
            var result = _userProfileService.Update(userProfile);
            return CreateResponse(result);
        }

        private long GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst("id")?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }

            return long.Parse(userIdClaim);
        }
    }
}
