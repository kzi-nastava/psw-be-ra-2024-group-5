using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.BuildingBlocks.Core.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost]
        //public ActionResult<ClubMembershipDto> Create([FromBody] ClubMembershipDto clubMembership)
        public ActionResult CreateMembership(int clubId, int userId)
        {
            long ciD = (long)clubId;
            long tId = (long)userId;

            var result = _clubService.CreateMembership(ciD, tId);
            return CreateResponse(result);
        }
        //public ActionResult CreateClubMembership(int tourId, [FromBody] List<int> equipmentIds)
        //{
        //    long id = (long)tourId;
        //    List<long> ids = equipmentIds.Select(id => (long)id).ToList();

        //    var result = _tourService.UpdateTourEquipment(id, ids);
        //    return CreateResponse(result);

        [HttpDelete]
        //public ActionResult Delete(int id)
        public ActionResult DeleteMembership(int clubId, int userId)
        {
            long ciD = (long)clubId;
            long tId = (long)userId;

            var result = _clubService.DeleteMembership(ciD, tId);
            return CreateResponse(result);
        }
        //public ActionResult DeleteClubMembership(int tourId, [FromBody] List<int> equipmentIds)
        //{
        //    long id = (long)tourId;
        //    List<long> ids = equipmentIds.Select(id => (long)id).ToList();

        //    var result = _tourService.UpdateTourEquipment(id, ids);
        //    return CreateResponse(result);
    }
}
