using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Administrator.Administration;

[Authorize(Policy = "administratorPolicy")]
[Route("api/administration/encounter")]
public class EncounterController : BaseApiController
{
    private readonly IEncounterService _encounterService;

    public EncounterController(IEncounterService encounterService)
    {
        _encounterService = encounterService;
    }

    [HttpGet("creator/{creatorId:long}")]
    public ActionResult<List<EncounterDto>> GetByCreatorId(long creatorId)
    {
        var result = _encounterService.GetByCreatorId(creatorId);
        return CreateResponse(result);
    }

    [HttpPost]
    public ActionResult<EncounterDto> Create([FromBody] EncounterDto encounter)
    {
        var result = _encounterService.Create(encounter);
        return CreateResponse(result);
    }

    [HttpPut("{id:long}")]
    public ActionResult<EncounterDto> Update([FromBody] EncounterDto encounter)
    {
        var result = _encounterService.Update(encounter);
        return CreateResponse(result);
    }

    [HttpDelete("{id:long}")]
    public ActionResult Delete(long id)
    {
        var result = _encounterService.Delete((int) id);
        return CreateResponse(result);
    }
}
