using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist;

[Authorize(Policy = "touristPolicy")]
[Route("api/tourist/encounter/execution")]
public class EncounterExecutionController : BaseApiController {
    private readonly IEncounterExecutionService _encounterExecutionService;

    public EncounterExecutionController(IEncounterExecutionService encounterExecutionService) {
        _encounterExecutionService = encounterExecutionService;
    }

    [HttpGet("{userId:long}")]
    public ActionResult<EncounterDto> GetActive(long userId) {
        var result = _encounterExecutionService.GetActiveEncounter(userId);
        return CreateResponse(result);
    }

    [HttpPost]
    public ActionResult<EncounterDto> Start([FromBody] EncounterExecutionRequestDto request) {
        var result = _encounterExecutionService.Start(request);
        return CreateResponse(result);
    }

    [HttpPost("check-availability")]
    public ActionResult IsAvailable([FromBody] EncounterExecutionRequestDto request) {
        var result = _encounterExecutionService.IsAvailable(request);
        return CreateResponse(result);
    }

    [HttpPatch]
    public ActionResult Progress([FromBody] EncounterExecutionRequestDto request) {
        var result = _encounterExecutionService.Progress(request);
        return CreateResponse(result);
    }

    [HttpDelete("{userId:long}")]
    public ActionResult Abandon(long userId) {
        var result = _encounterExecutionService.Abandon(userId);
        return CreateResponse(result);
    }
}

