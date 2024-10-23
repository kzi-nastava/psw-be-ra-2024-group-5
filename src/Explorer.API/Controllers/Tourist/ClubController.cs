using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
        [Authorize(Policy = "touristPolicy")]
        [Route("api/tourist/club")]
        public class ClubController : BaseApiController
        {
            private readonly IClubService _clubService;

            public ClubController(IClubService clubService)
            {
                _clubService = clubService;
            }

            [HttpGet]
            public ActionResult<PagedResult<ClubDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
            {
                var result = _clubService.GetPaged(page, pageSize);
                return CreateResponse(result);
            }

            [HttpPost]
            public ActionResult<ClubDto> Create([FromBody] ClubDto club)
            {
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "personId");
                if (userIdClaim == null)
                {
                    return BadRequest("user ID not found.");
                }
                // Assign the extracted user ID to the ownerId field
                var userId = long.Parse(userIdClaim.Value);
                club.OwnerId = userId;

            var result = _clubService.Create(club);
                return CreateResponse(result);
            }

            [HttpPut("{id:int}")]
            public ActionResult<ClubDto> Update([FromBody] ClubDto club)
            {
                var result = _clubService.Update(club);
                return CreateResponse(result);
            }

            [HttpDelete("{id:int}")]
            public ActionResult Delete(int id)
            {
                var result = _clubService.Delete(id);
                return CreateResponse(result);
            }
        }
}
