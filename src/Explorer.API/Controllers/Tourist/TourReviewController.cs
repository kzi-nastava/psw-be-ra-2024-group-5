using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos.TourLifecycle;
using Explorer.Tours.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/tour-reviews")]
    public class TourReviewController : BaseApiController
    {
        private readonly ITourReviewService _tourReviewService;

        public TourReviewController(ITourReviewService tourReviewService)
        {
            _tourReviewService = tourReviewService;
        }

        [HttpPost]
        public ActionResult<TourReviewDto> Create([FromBody] TourReviewDto review)
        {
            var result = _tourReviewService.Create(review);
            return CreateResponse(result);
        }

        [HttpGet("{id:int}")]
        public ActionResult<TourReviewDto> GetById(int id)
        {
            var result = _tourReviewService.GetById(id);
            return CreateResponse(result);
        }

        [HttpGet("by-tour/{tourId:int}")]
        public ActionResult<List<TourReviewDto>> GetByTourId(int tourId, [FromQuery] long? userId)
        {
            var result = _tourReviewService.GetByTourId(tourId, userId);
            return CreateResponse(result);
        }

        [HttpGet("by-tourist/{touristId:int}")]
        public ActionResult<List<TourReviewDto>> GetByTouristId(int touristId)
        {
            var result = _tourReviewService.GetByTouristId(touristId);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<TourReviewDto> Update([FromBody] TourReviewDto review)
        {
            var result = _tourReviewService.Update(review);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _tourReviewService.Delete(id);
            return CreateResponse(result);
        }
    }
}