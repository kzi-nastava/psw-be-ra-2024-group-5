using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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