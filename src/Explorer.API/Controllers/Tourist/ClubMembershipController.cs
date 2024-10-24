using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.BuildingBlocks.Core.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Explorer.Stakeholders.Core.Domain;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/membership")]
    //[Route("api/tours/{tourId:int}/equipment")]
    public class ClubMembershipController : BaseApiController
    {
        private readonly IClubService _clubService;

        public ClubMembershipController(IClubService clubService)
        {
            _clubService = clubService;
        }

        [HttpGet]
        public ActionResult<List<ClubMembership>> GetAllMemberships()
        {
            var result = _clubService.GetAllMemberships();
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult CreateMembership(int clubId, int userId)
        {
            long ciD = (long)clubId;
            long tId = (long)userId;

            var result = _clubService.CreateMembership(ciD, tId);
            return CreateResponse(result);
        }

        [HttpDelete]
        public ActionResult DeleteMembership(int clubId, int userId)
        {
            long ciD = (long)clubId;
            long tId = (long)userId;

            var result = _clubService.DeleteMembership(ciD, tId);
            return CreateResponse(result);
        }
    }
}
