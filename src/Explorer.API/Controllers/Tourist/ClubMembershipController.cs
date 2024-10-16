using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.BuildingBlocks.Core.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/membership")]
    public class ClubMembershipController : BaseApiController
    {
        private readonly IClubMembershipService _clubMembershipService;

        public ClubMembershipController(IClubMembershipService clubMembershipService)
        {
            _clubMembershipService = clubMembershipService;
        }

        [HttpGet]
        public ActionResult<PagedResult<ClubMembershipDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _clubMembershipService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<ClubMembershipDto> Create([FromBody] ClubMembershipDto clubMembership)
        {
            var result = _clubMembershipService.Create(clubMembership);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _clubMembershipService.Delete(id);
            return CreateResponse(result);
        }
    }
}
