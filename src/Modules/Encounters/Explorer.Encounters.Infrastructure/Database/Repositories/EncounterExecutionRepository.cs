using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Encounters.API.Enum;
using Explorer.Encounters.Core.Domain;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Infrastructure.Database.Repositories {
    public class EncounterExecutionRepository : CrudDatabaseRepository<EncounterExecution, EncountersContext>, IEncounterExecutionRepository {
        public EncounterExecutionRepository(EncountersContext context) : base(context) { }

        public EncounterExecution? GetActive(long userId) {
            return DbContext.EncountersExecution.FirstOrDefault(e => e.UserId == userId && e.Status == EncounterExecutionStatus.Active);
        }

        public bool IsCompleted(long userId, long encounterId)
        {
            return DbContext.EncountersExecution
                .Any(e => e.UserId == userId && e.EncounterId == encounterId && e.Status == EncounterExecutionStatus.Completed);
        }
    }
}
