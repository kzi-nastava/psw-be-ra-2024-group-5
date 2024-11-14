using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces;

public interface ITourRepository : ICrudRepository<Tour> {
    Result UpdateTourEquipment(long tourId, List<long> equipmentIds);
    Result<PagedResult<Equipment>> GetTourEquipment(long tourId);
    Tour? GetById(int id);
    List<Tour>? GetByAuthorId(int authorId);
    List<Tour> GetPublishedPaged(int page, int pageSize);

    public List<Tour> GetPublishedPagedFiltered(int page, int pageSize, double startLong, double endLong, double startLat, double endLat);
}