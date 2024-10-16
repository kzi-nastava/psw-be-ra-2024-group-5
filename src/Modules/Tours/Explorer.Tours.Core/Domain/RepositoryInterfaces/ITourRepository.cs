using Explorer.BuildingBlocks.Core.UseCases;
using FluentResults;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces;

public interface ITourRepository : ICrudRepository<Tour>
{
    Result UpdateTourEquipment(long tourId, List<long> equipmentIds);
}