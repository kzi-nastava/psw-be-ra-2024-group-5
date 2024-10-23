using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
            return NotFound(); 
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(int pageIndex = 0, int pageSize = 10)
        {
            // Ekstrakcija user ID-a iz JWT tokena
            var userId = User.FindFirst("id")?.Value;  

            if (userId == null)
            {
                return BadRequest("User ID not found.");
            }

            // Pretvaranje userId u long
            if (!long.TryParse(userId, out long parsedUserId))
            {
                return BadRequest("Invalid user ID.");
            }

            var result = await _preferenceService.GetPagedByUserId(parsedUserId, pageIndex, pageSize);

            if (result.IsFailed)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result.Value);
        }



    }
}
