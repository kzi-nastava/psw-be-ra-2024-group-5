using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Preferences.Core.Domain;
using Explorer.Preferences.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Infrastructure.Database;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Preferences.Infrastructure.Database.Repositories;

public class PreferenceRepository : CrudDatabaseRepository<Preference, ToursContext>, IPreferenceRepository
{
    private readonly ToursContext _dbContext;

    public PreferenceRepository(ToursContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public Preference? GetById(long id)
    {
        return _dbContext.Preferences
            .Where(p => p.Id == id)
            .FirstOrDefault();
    }

    public Preference AddPreference(Preference preference)
    {
        _dbContext.Preferences.Add(preference);
        _dbContext.SaveChanges();
        return preference;
    }

    public Result ActivatePreference(long preferenceId)
    {
        var preference = _dbContext.Preferences.FirstOrDefault(p => p.Id == preferenceId);
        if (preference == null)
        {
            return Result.Fail("Preference not found.");
        }

        preference.Activate();
        _dbContext.Entry(preference).State = EntityState.Modified;
        _dbContext.SaveChanges();

        return Result.Ok();
    }

    public Result DeactivatePreference(long preferenceId)
    {
        var preference = _dbContext.Preferences.FirstOrDefault(p => p.Id == preferenceId);
        if (preference == null)
        {
            return Result.Fail("Preference not found.");
        }

        preference.Deactivate();
        _dbContext.Entry(preference).State = EntityState.Modified;
        _dbContext.SaveChanges();

        return Result.Ok();
    }

    public List<Preference> GetByTouristId(long touristId)
    {
        return _dbContext.Preferences
            .Where(p => p.TouristId == touristId)
            .ToList();
    }

    public List<Preference> GetAll()
    {
        return _dbContext.Preferences.ToList();
    }

}
