using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;

namespace Explorer.Tours.Infrastructure.Database.Repositories;

public class EquipmentRepository : CrudDatabaseRepository<Equipment, ToursContext>, IEquipmentRepository
{
    private readonly ToursContext _dbContext;

    public EquipmentRepository(ToursContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public Equipment? FindByName(string name)
    {
        var e = _dbContext.Equipment.FirstOrDefault(
            e => e.Name.ToLower().Contains(name.ToLower()));
        return e;
    }
}