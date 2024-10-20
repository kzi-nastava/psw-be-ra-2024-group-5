using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/preference")]
    public class PreferenceController : BaseApiController
    {
        private readonly IPreferenceService _preferenceService;

        public PreferenceController(IPreferenceService preferenceService)
        {
            _preferenceService = preferenceService;
        }

        [HttpPost]
        public ActionResult<PreferenceDto> Create([FromBody] PreferenceDto preference)
        {
            var result = _preferenceService.Create(preference);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<PreferenceDto> Update([FromBody] PreferenceDto preference)
        {
            var result = _preferenceService.Update(preference);
            return CreateResponse(result);
        } 

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _preferenceService.Delete(id);
            if (result.IsSuccess)
            {
                return Ok(); // Vraća 200 OK
            }
            return NotFound(); // Vraća 404 Not Found ako nije pronađena preferenca
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int pageIndex = 0, int pageSize = 10)
        {
            var result = _preferenceService.GetPaged(pageIndex, pageSize);

            if (result.IsFailed)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result.Value);
        }


    }
}
