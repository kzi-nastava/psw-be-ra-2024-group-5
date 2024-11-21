using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.TourLifecycle;
using Explorer.Tours.API.Enum;
using FluentResults;

namespace Explorer.Tours.API.Public.Administration;

public interface ITourService {
    Result<List<TourCardDto>> GetByAuthorPaged(int authorId, int page, int pageSize);
    Result<TourDto> GetById(long id);
    Result<TourDto> Create(TourCreationDto tour);
    Result<TourDto> Update(TourDto tour, long id);
    Result Delete(int Id);
    Result UpdateTourEquipment(long tourId, List<long> equipmentId);
    Result<PagedResult<EquipmentDto>> GetTourEquipment(long tourId);
    Result PublishTour(long tourId, double priceAmount, Currency currency);

    Result<List<TourCardDto>> GetPublishedPagedTours(int page, int pageSize);
    Result<List<TourCardDto>> GetPublishedPagedToursFiltered(int page, int pageSize, double startLong, double endLong, double startLat, double endLat);
    Result<TourReviewDto> AddReview(TourReviewDto reviewDto);
    Result<TourTouristDto> GetForTouristById(long id, long touristId);
    Result ArchiveTour(long tourId);

}