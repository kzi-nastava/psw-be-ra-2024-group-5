using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Encounters.API.Enum;
using Explorer.Encounters.Core.Domain;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Infrastructure.Database.Repositories;
public class EncounterRepository : CrudDatabaseRepository<Encounter, EncountersContext>, IEncounterRepository
{
    public EncounterRepository(EncountersContext dbContext) : base(dbContext) { }

    public List<Encounter> GetAllActive() {
        return DbContext.Encounters.Where(e => e.Status == EncounterStatus.Active).ToList();
    }

    public List<Encounter> GetByCreatorId(long creatorId) {
        return DbContext.Encounters.Where(e => e.CreatorId == creatorId).ToList();
    }

    public List<Encounter> GetAllDraft()
    {
        return DbContext.Encounters.Where(e => e.Status == EncounterStatus.Draft).ToList();
    }
}
