using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Tours.API.Dtos.TourLifecycle;
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

    public Tour? GetById(long id) {
        return DbContext.Tours.Where(t => t.Id == id)
            .Include(t => t.KeyPoints)
            .Include(t => t.Reviews)
            .FirstOrDefault();
    }

    public List<Tour>? GetByAuthorPaged(int authorId, int page, int pageSize) {
        if (page < 1 || pageSize < 1) {
            return new List<Tour>();
        }

        return DbContext.Tours.Where(t => t.AuthorId == authorId)
            .Include(t => t.KeyPoints)
            .Include(t => t.Reviews)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();
    }
    public List<Tour> GetAuthorPagedToursFiltered(int authorId, int page, int pageSize, double startLong, double endLong, double startLat, double endLat) {
        if (page < 1 || pageSize < 1) {
            return new List<Tour>();
        }

        var allTours = DbContext.Tours.Where(t => t.AuthorId == authorId)
            .Include(t => t.KeyPoints)
            .Include(t => t.Reviews)
            .ToList();

        // Use the Tour aggregate's filtering method
        var filteredTours = Tour.FilterToursByLocation(allTours, startLat, endLat, startLong, endLong);

        return filteredTours
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();
    }


    public new Tour Update(Tour tour) {
        DbContext.Entry(tour).State = EntityState.Modified;
        DbContext.SaveChanges();
        return tour;
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


    public Result<PagedResult<Equipment>> GetTourEquipment(long tourId) {
        var tourEquipment = _dbContext.TourEquipment.ToList();
        var allEquipment = _dbContext.Equipment.ToList();
        var equipment = new List<Equipment>();

        foreach (var te in tourEquipment)
            if (te.TourId == tourId) {
                var foundEquipment = allEquipment.FirstOrDefault(e => e.Id == te.EquipmentId);
                if (foundEquipment != null)
                    equipment.Add(foundEquipment);
            }

        return new PagedResult<Equipment>(equipment, equipment.Count);
    }

    public List<Tour> GetPublishedPaged(int page, int pageSize)
    {
        if (page < 1 || pageSize < 1)
        {
            return new List<Tour>();
        }

        return DbContext.Tours
            .Where(t => t.Status == API.Enum.TourStatus.Published)
            .Include(t => t.KeyPoints)
            .Include(t => t.Reviews)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();
    }

    public List<Tour> GetPublishedPagedFiltered(int page, int pageSize, double startLong, double endLong, double startLat, double endLat)
    {
        if (page < 1 || pageSize < 1)
        {
            return new List<Tour>();
        }

        var allTours = DbContext.Tours
            .Where(t => t.Status == API.Enum.TourStatus.Published)
            .Include(t => t.KeyPoints)
            .ToList();

        // Use the Tour aggregate's filtering method
        var filteredTours = Tour.FilterToursByLocation(allTours, startLat, endLat, startLong, endLong);

        return filteredTours
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();
    }

	public async Task<List<Tour>> GetToursByIds(List<long> tourIds)
	{
		// Pretražuje bazu podataka i vra?a ture koje odgovaraju ID-evima
		return await _dbContext.Tours
			.Where(t => tourIds.Contains(t.Id))
			.ToListAsync();
	}
}