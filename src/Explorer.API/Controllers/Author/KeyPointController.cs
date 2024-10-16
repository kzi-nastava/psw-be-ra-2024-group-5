using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author {
    [Authorize(Policy = "authorPolicy")]
    [Route("api/keypoint")]
    public class KeyPointController : BaseApiController {
        private readonly IKeyPointService _keyPointService;

        public KeyPointController(IKeyPointService keyPointService) {
            _keyPointService = keyPointService;
        }

        /* [HttpPost]
        public ActionResult<KeyPointDto> Create([FromForm] KeyPointDto keyPoint) {
            var result = _keyPointService.Create(keyPoint);
            return CreateResponse(result);
        } */
    }
}
