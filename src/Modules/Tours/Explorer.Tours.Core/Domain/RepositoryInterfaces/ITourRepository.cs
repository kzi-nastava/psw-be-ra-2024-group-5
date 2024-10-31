using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces;

public interface ITourRepository : ICrudRepository<Tour> {
    Result UpdateTourEquipment(long tourId, List<long> equipmentIds);
    Result<PagedResult<Equipment>> GetTourEquipment(long tourId);

    Tour? GetById(int id);
    List<Tour>? GetByAuthorId(int authorId);
}