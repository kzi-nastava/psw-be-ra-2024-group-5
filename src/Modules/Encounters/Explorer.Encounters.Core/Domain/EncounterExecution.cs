using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Encounters.API.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.Domain;
public class EncounterExecution : Entity {

    public long UserId { get; init; }
    public long EncounterId { get; init; }
    public EncounterExecutionStatus Status { get; private set; } = EncounterExecutionStatus.Active;
    public DateTime? SessionEnd { get; private set; } = null;

    public EncounterExecution() { }
    public EncounterExecution(long userId, long encounterId) {
        UserId = userId;
        EncounterId = encounterId;
    }

    public void Complete() {
        Status = EncounterExecutionStatus.Completed;
        SessionEnd = DateTime.UtcNow;
    }

    public void Abandon() {
        Status = EncounterExecutionStatus.Abandoned;
        SessionEnd = DateTime.UtcNow;
    }
}