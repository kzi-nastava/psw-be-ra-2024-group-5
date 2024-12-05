using FluentResults;
using Explorer.Preferences.Core.Domain;
using Explorer.Tours.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;

namespace Explorer.Preferences.Core.Domain.RepositoryInterfaces;

public interface IPreferenceRepository : ICrudRepository<Preference>
{
    Preference? GetById(long id);
    Preference AddPreference(Preference preference);
    Result ActivatePreference(long preferenceId);
    Result DeactivatePreference(long preferenceId);
    List<Preference> GetByTouristId(long touristId);
    List<Preference> GetAll();
}
