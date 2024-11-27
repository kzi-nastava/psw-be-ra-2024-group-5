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

        [HttpGet("{id:int}")]
        public ActionResult<UserProfileDto> GetProfile(long id)
        {
            try
            {
                var result = _userProfileService.Get(id);

                if (result.IsSuccess)
                {
                    var userProfileDto = result.Value;

                    if (string.IsNullOrEmpty(userProfileDto.Biography) && string.IsNullOrEmpty(userProfileDto.Motto))
                    {
                        userProfileDto.Biography = string.Empty;
                        userProfileDto.Motto = string.Empty;
                    }

                    return CreateResponse(result);
                }

                return NotFound($"Profile with id: {id} not found.");
            }
            catch (Exception e)
            {
                if (e is NullReferenceException || e is InvalidOperationException) 
                {
                    
                    var userProfileDto = new UserProfileDto
                    {
                        Biography = string.Empty,
                        Motto = string.Empty
                    };

                    return Ok(userProfileDto); 
                }

                return StatusCode(500, $"Internal server error: {e.Message}");
            }
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
            var result = _userProfileService.Update(GetCurrentUserId(), userProfile);
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

        [HttpGet("basic-profiles/{userIds}")]
        public ActionResult<List<UserProfileBasicDto>> GetBasicProfiles(string userIds)
        {
            try
            {
                var idList = userIds.Split(',').Select(long.Parse).ToList();
                var result = _userProfileService.GetBasicProfiles(idList);
                return CreateResponse(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal server error: {e.Message}");
            }
        }
    }
}
