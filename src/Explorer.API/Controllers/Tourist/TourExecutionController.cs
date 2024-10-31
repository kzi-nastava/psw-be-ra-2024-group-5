using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.API.Public.Tourist;
using Explorer.Tours.Core.UseCases.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist {

    [Authorize(Policy = "touristPolicy")]
    [Route("api/tours/{tourId}/executions")]
    public class TourExecutionController : BaseApiController {
        private readonly ITourExecutionService _tourExecutionService;

        public TourExecutionController(ITourExecutionService tourExecutionService) {
            _tourExecutionService = tourExecutionService;
        }

        [HttpPost]
        public ActionResult<TourExecutionDto> Create(long tourId, [FromBody] TourExecutionDto tourExecution) {
            var result = _tourExecutionService.StartTourExecution(tourId, tourExecution);
            return CreateResponse(result);
        }
    }
}
