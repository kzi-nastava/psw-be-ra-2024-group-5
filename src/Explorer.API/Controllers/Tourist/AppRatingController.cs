using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Explorer.API.Controllers.Tourist
{
    [Route("api/tourist/appRating")]
    [ApiController]
    public class AppRatingController : BaseApiController
    {
        private readonly IAppRatingService _appRatingService;

        public AppRatingController(IAppRatingService appRatingService)
        {
            _appRatingService = appRatingService;
        }
        [Authorize(Policy = "touristPolicy")]
        [HttpGet("user")]
        public ActionResult<AppRatingDto> GetByUser()
        {
            try
            {
                // Check if we have any claims
                var claims = User.Claims.ToList();
                if (!claims.Any())
                {
                    return BadRequest("No claims found in token");
                }

                // Try to get the ID claim
                var idClaim = User.FindFirst("id");
                if (idClaim == null)
                {
                    return BadRequest("ID claim not found in token");
                }

                var userId = long.Parse(idClaim.Value);
                if (userId == 0)
                {
                    return BadRequest("Invalid user ID");
                }

                var result = _appRatingService.GetByUser(userId);
                if (result.IsFailed)
                {
                    // If rating not found, return 404 instead of 500
                    if (result.Errors.Any(e => e.Message == "Rating not found"))
                    {
                        return NotFound("No rating found for this user");
                    }
                    return BadRequest(result.Errors.FirstOrDefault()?.Message);
                }

                return CreateResponse(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [Authorize(Policy = "administratorPolicy")]
        [HttpGet]
        public ActionResult<PagedResult<AppRatingDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _appRatingService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<AppRatingDto> Create([FromBody] AppRatingDto applicationReview)
        {
            var result = _appRatingService.Create(applicationReview);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<AppRatingDto> Update([FromBody] AppRatingDto applicationReview)
        {
            var result = _appRatingService.Update(applicationReview);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _appRatingService.Delete(id);
            return CreateResponse(result);
        }
    }
}