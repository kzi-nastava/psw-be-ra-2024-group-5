using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;

namespace Explorer.Tours.API.Public.Administration;

public interface ITourService {
    public Result<PagedResult<TourDto>> GetByAuthorId(long id);
    Result<TourDto> Create(TourDto tour);
    Result<TourDto> Update(TourDto tour);
    Result Delete(int Id);
    Result UpdateTourEquipment(long tourId, List<long> equipmentId);
}