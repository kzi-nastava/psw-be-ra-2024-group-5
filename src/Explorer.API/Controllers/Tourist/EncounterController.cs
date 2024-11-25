using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Enum;
using Explorer.Encounters.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist;

[Authorize(Policy = "touristPolicy")]
[Route("api/tourist/encounter")]
public class EncounterController : BaseApiController
{
    private readonly IEncounterService _encounterService;

    public EncounterController(IEncounterService encounterService)
    {
        _encounterService = encounterService;
    }

    [HttpGet("active")]
    public ActionResult<List<EncounterDto>> GetAllActive()
    {
        var result = _encounterService.GetAllActive();
        return CreateResponse(result);
    }
}
