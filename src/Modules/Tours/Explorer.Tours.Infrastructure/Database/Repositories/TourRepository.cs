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

    public Result UpdateTourEquipment(long tourId, List<long> equipmentIds)
    {
        var tourEquipment = _dbContext.TourEquipment;
        
        foreach (var te in tourEquipment)
            if (te.TourId == tourId)
                tourEquipment.Remove(te);
        
        foreach (var id in equipmentIds)
            tourEquipment.Add(new TourEquipment(tourId, id));
        
        _dbContext.SaveChanges();

        return Result.Ok();
    }
}