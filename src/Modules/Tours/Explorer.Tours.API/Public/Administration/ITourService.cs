

using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;

namespace Explorer.Tours.API.Public.Administration;

public interface ITourService {
    Result<List<TourDto>> GetByAuthorId(int id);
    Result<TourDto> GetById(int id);
    Result<TourDto> Create(TourCreationDto tour);
    Result<TourDto> Update(TourDto tour, long id);
    Result Delete(int Id);
    Result UpdateTourEquipment(long tourId, List<long> equipmentId);
    Result<PagedResult<EquipmentDto>> GetTourEquipment(long tourId);
    Result PublishTour(int tourId);

    Result<List<TourCardDto>> GetPublishedPagedTours(int page, int pageSize);
    Result<List<TourCardDto>> GetPublishedPagedToursFiltered(int page, int pageSize, double startLong, double endLong, double startLat, double endLat);
    Result<TourReviewDto> AddReview(TourReviewDto reviewDto);
    Result<TourTouristDto> GetForTouristById(long id, long touristId);
    Result ArchiveTour(int tourId);

}