

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
    Result<List<TourCardDto>> GetPublishedPagedTours(int page, int pageSize);
}