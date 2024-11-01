using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author {
    [Authorize(Policy = "authorPolicy")]
    [Route("api/tour")]
    public class TourController : BaseApiController {
        private readonly ITourService _tourService;


        public TourController(ITourService tourService) {
            _tourService = tourService;
        }

        [HttpGet("author/{authorId:long}")]
        public ActionResult<List<TourDto>> GetByAuthor(int authorId) {
            var result = _tourService.GetByAuthorId(authorId);
            return CreateResponse(result);
        }

        [HttpGet("{id:int}")]
        public ActionResult<TourDto> GetById(int id) {
            var result = _tourService.GetById(id);
            return CreateResponse(result);
        }

        [AllowAnonymous]
        [HttpGet("published/{page:int}/{pageSize:int}")]
        public ActionResult<List<TourCardDto>> GetPublishedPagedTours(int page, int pageSize /*bool flag, int startLOng endLong, int startlat endLat*/)
        {
            var result = _tourService.GetPublishedPagedTours(page,pageSize);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<TourDto> Create([FromBody] TourCreationDto tour) {
            var result = _tourService.Create(tour);
            return CreateResponse(result);
        }

        [HttpPut("{id:long}")]
        public ActionResult<TourDto> Update([FromBody] TourDto tour, long id) {
            var result = _tourService.Update(tour, id);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id) {
            var result = _tourService.Delete(id);
            return CreateResponse(result);
        }
    }
}
