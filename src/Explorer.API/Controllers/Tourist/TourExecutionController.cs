using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos.TourExecution;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.API.Public.Tourist;
using Explorer.Tours.Core.UseCases.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{

    [Authorize(Policy = "touristPolicy")]
    [Route("api/tour/execution")]
    public class TourExecutionController : BaseApiController {
        private readonly ITourExecutionService _tourExecutionService;

        public TourExecutionController(ITourExecutionService tourExecutionService) {
            _tourExecutionService = tourExecutionService;
        }

        [HttpGet("{UserId:long}")]
        public ActionResult<TourExecutionDto> GetActive(long userId) {
            var result = _tourExecutionService.GetActive(userId);
            return CreateResponse(result);
        }

        [HttpGet("completion/{tourExecutionId:long}")]
        public ActionResult<bool> IsTourCompleted(long tourExecutionId) {
            var result = _tourExecutionService.IsTourCompleted(tourExecutionId);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<TourExecutionDto> Start([FromBody] TourExecutionStartDto tourExecutionStartDto) {
            var result = _tourExecutionService.Start(tourExecutionStartDto);
            return CreateResponse(result);
        }

        [HttpPatch("{tourExecutionId:long}")]
        public ActionResult<KeyPointProgressDto> Progress(long tourExecutionId, [FromBody] PositionDto postitionDto) {
            var result = _tourExecutionService.Progress(tourExecutionId, postitionDto);
            return CreateResponse(result);
        }

        [HttpPatch("abandon/{tourExecutionId:long}")]
        public ActionResult<bool> Abandon(long tourExecutionId) {
            var result = _tourExecutionService.Abandon(tourExecutionId);
            return CreateResponse(result);
        }
    }
}
