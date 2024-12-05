using Explorer.BuildingBlocks.Core.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.Domain.RepositoryInterfaces {
    public interface IEncounterExecutionRepository : ICrudRepository<EncounterExecution> {
        public EncounterExecution? GetActive(long userId);
        public List<long> GetCompletedEncounterIds(long userId);
        public bool IsCompleted(long userId, long encounterId);
    }
}
