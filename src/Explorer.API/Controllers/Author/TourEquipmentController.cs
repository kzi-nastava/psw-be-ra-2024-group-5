using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.UseCases.Administration;

namespace Explorer.API.Controllers.Author;

[Authorize(Policy = "authorPolicy")]
[Route("api/tour/equipment")]
public class TourEquipmentController : BaseApiController
{
    private readonly ITourService _tourService;
    private readonly IEquipmentService _equipmentService;

    public TourEquipmentController(ITourService tourService, IEquipmentService equipmentService) {
        _tourService = tourService;
        _equipmentService = equipmentService;
    }

    [HttpGet]
    public ActionResult<PagedResult<EquipmentDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize) {
        var result = _equipmentService.GetPaged(page, pageSize);
        return CreateResponse(result);
    }

    [HttpPut("{tourId:int}")]
    public ActionResult UpdateTourEquipment(int tourId, [FromBody] List<int> equipmentIds) {
        long id = (long)tourId;
        List<long> ids = equipmentIds.Select(id => (long)id).ToList();

        var result = _tourService.UpdateTourEquipment(id, ids);
        return CreateResponse(result);
    }
}