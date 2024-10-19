using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author {
    [Authorize(Policy = "authorPolicy")]
    [Route("api/tours/{tourId}/keypoints")]
    public class KeyPointController : BaseApiController {
        private readonly IKeyPointService _keyPointService;

        public KeyPointController(IKeyPointService keyPointService) {
            _keyPointService = keyPointService;
        }

        [HttpPost]
        public ActionResult<List<KeyPointDto>> Create(int tourId, [FromBody] List<KeyPointDto> keyPoints) {
            var result = _keyPointService.Create(tourId, keyPoints);
            return CreateResponse(result);
        } 
    }
}
