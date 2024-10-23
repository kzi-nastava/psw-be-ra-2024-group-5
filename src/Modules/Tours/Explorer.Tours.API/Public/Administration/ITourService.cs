

using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;

namespace Explorer.Tours.API.Public.Administration;

public interface ITourService {
    Result<PagedResult<TourDto>> GetByAuthorId(long id);
    Result<TourDto> GetById(int id);
    Result<TourDto> CreateTour(TourDto tour);
    Result<TourDto> Update(TourDto tour);
    Result Delete(int Id);
    Result UpdateTourEquipment(long tourId, List<long> equipmentId);
    Result<PagedResult<EquipmentDto>> GetTourEquipment(long tourId);
}