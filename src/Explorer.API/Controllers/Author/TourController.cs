using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos.TourLifecycle;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FluentResults;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Explorer.Tours.API.Dtos.TourLeaderboard;

namespace Explorer.API.Controllers.Author
{
    [Authorize(Policy = "authorPolicy")]
    [Route("api/tour")]
    public class TourController : BaseApiController {
        private readonly ITourService _tourService;


        public TourController(ITourService tourService) {
            _tourService = tourService;
        }

        [HttpGet("author/{authorId:long}/{page:int}/{pageSize:int}")]
        public ActionResult<List<TourCardDto>> GetByAuthorPaged(int authorId, int page, int pageSize) {
            var result = _tourService.GetByAuthorPaged(authorId, page, pageSize);
            return CreateResponse(result);
        }

        [HttpPost("author/filtered/{authorId:int}")]
        public ActionResult<List<TourCardDto>> GetAuthorPagedToursFiltered(int authorId, [FromBody] TourSearchDto searchDto) {
            var result = _tourService.GetAuthorPagedToursFiltered(
                authorId,
                searchDto.Page,
                searchDto.PageSize,
                searchDto.StartLong ?? -180, 
                searchDto.EndLong ?? 180,    
                searchDto.StartLat ?? -90,   
                searchDto.EndLat ?? 90
            );
            return CreateResponse(result);
        }

        [HttpGet("{id:int}")]
        public ActionResult<TourDto> GetById(int id) {
            var result = _tourService.GetById(id);
            return CreateResponse(result);
        }

        [AllowAnonymous]
        [Authorize(Policy = "touristPolicy")]
        [HttpGet("tourist/{id:long}/{touristId:long}")]
        public ActionResult<TourTouristDto> GetForTouristById(long id, long touristId)
        {
            var result = _tourService.GetForTouristById(id, touristId);
            return CreateResponse(result);
        }

        [AllowAnonymous]
        [Authorize(Policy = "touristPolicy")]
        [HttpPost("review")]
        public ActionResult<TourReviewDto> AddReview([FromBody] TourReviewDto review)
        {
            var result = _tourService.AddReview(review);
            return CreateResponse(result);
        }

        [AllowAnonymous]
        [Authorize(Policy = "touristPolicy")]
        [HttpGet("{tourId:int}/leaderboard")]
        public ActionResult<TourLeaderboardDto?> GetLeaderboard(int tourId)
        {
            var result = _tourService.GetLeaderboard(tourId);
            return CreateResponse(result);
        }

        [AllowAnonymous]
        [HttpGet("published/{page:int}/{pageSize:int}")]
        public ActionResult<List<TourCardDto>> GetPublishedPagedTours(int page, int pageSize /*bool flag, int startLOng endLong, int startlat endLat*/)
        {
            var result = _tourService.GetPublishedPagedTours(page, pageSize);
            return CreateResponse(result);
        }
        [AllowAnonymous]
        [Authorize(Policy = "touristPolicy")]
        [HttpPost("published/filtered")]
        public ActionResult<List<TourCardDto>> GetPublishedPagedToursFiltered([FromBody] TourSearchDto searchDto)
        {
            var result = _tourService.GetPublishedPagedToursFiltered(
                searchDto.Page,
                searchDto.PageSize,
                searchDto.StartLong,
                searchDto.EndLong,
                searchDto.StartLat,
                searchDto.EndLat,
                searchDto.Name,
                searchDto.Length,
                searchDto.MinPrice,
                searchDto.MaxPrice

            );
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

        [HttpPost("publish/{tourId:long}")]
        public ActionResult PublishTour(long tourId, [FromBody] MoneyDto money)
        {
            // Proveriti da li su prosleđeni podaci validni
            if (money == null || money.Amount <= 0 || money.Currency == null)
            {
                return BadRequest("Invalid price or currency.");
            }

            var result = _tourService.PublishTour(tourId, money.Amount, money.Currency);
            return CreateResponse(result);
        }

        [HttpPost("archive/{tourId:long}")]

        public ActionResult ArchiveTour(long tourId)
        {
            var result = _tourService.ArchiveTour(tourId);
            return CreateResponse(result);
        }

		[AllowAnonymous]
		[HttpGet("{tourId:long}/image")]
		public ActionResult GetTourImage(long tourId)
		{
			var result = _tourService.GetById(tourId);

			if (!result.IsSuccess || result.Value == null)
				return CreateResponse(Result.Fail("Tour not found."));

			var tour = result.Value;

			if (tour.KeyPoints == null || !tour.KeyPoints.Any())
				return CreateResponse(Result.Fail("KeyPoints not found."));

			var firstKeyPoint = tour.KeyPoints.FirstOrDefault();
			if (firstKeyPoint?.Image == null)
				return CreateResponse(Result.Fail("Image not available."));

			try
			{
				byte[] imageBytes = Convert.FromBase64String(firstKeyPoint.Image);
				return new FileContentResult(imageBytes, "image/jpeg");
			}
			catch (FormatException)
			{
				return CreateResponse(Result.Fail("Invalid Base64 string for the image."));
			}
		}

        [AllowAnonymous]
        [Authorize(Policy = "touristPolicy")]
        [HttpGet("tourist/{touristId:long}/preferences/{page:int}/{pageSize:int}")]
        public ActionResult<PagedResult<TourCardDto>> GetToursByActivePreferencePaged(long touristId, int page, int pageSize)
        {
            var result = _tourService.GetToursByActivePreferencePaged(touristId, page, pageSize);
            return CreateResponse(result);
        }
    }
}
