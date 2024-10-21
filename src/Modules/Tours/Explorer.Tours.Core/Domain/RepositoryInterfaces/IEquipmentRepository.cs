using Explorer.BuildingBlocks.Core.UseCases;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces;

public interface IEquipmentRepository : ICrudRepository<Equipment>
{
    public Equipment? FindByName(string name);
}