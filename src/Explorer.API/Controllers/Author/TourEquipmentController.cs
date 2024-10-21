using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Explorer.API.Controllers.Author;

[Authorize(Policy = "authorPolicy")]
[Route("api/tours/{tourId:int}/equipment")]
public class TourEquipmentController : BaseApiController
{
    private readonly ITourService _tourService;

    public TourEquipmentController(ITourService tourService)
    {
        _tourService = tourService;
    }
    
    [HttpPut]
    public ActionResult UpdateTourEquipment(int tourId, [FromBody] List<int> equipmentIds)
    {
        long id = (long)tourId;
        List<long> ids = equipmentIds.Select(id => (long)id).ToList();
            
        var result = _tourService.UpdateTourEquipment(id, ids);
        return CreateResponse(result);
    }
}