using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Tours.Infrastructure.Database.Repositories;

public class TourRepository : CrudDatabaseRepository<Tour, ToursContext>, ITourRepository
{
    private readonly ToursContext _dbContext;
    
    public TourRepository(ToursContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public Result UpdateTourEquipment(int tourId, List<int> equipmentIds)
    {
        var tour = _dbContext.Tours
            .FirstOrDefault(t => t.Id == tourId);
        
        if (tour is null)
            return Result.Fail("Tour not found");
        
        var new_equipment = _dbContext.Equipment
            .Where(e => equipmentIds.Contains((int) e.Id))
            .ToList();
        
        tour.Equipments = new_equipment;
        
        _dbContext.SaveChanges();

        return Result.Ok();
    }
}