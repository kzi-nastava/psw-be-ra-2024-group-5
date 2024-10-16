using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
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
            return CreateResponse(result);
        }
    }
}
