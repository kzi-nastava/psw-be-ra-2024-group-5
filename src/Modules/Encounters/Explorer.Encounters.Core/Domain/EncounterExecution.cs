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
    public DateTime LastActivity { get; private set; } = DateTime.UtcNow;

    public EncounterExecution() { }
    public EncounterExecution(long userId, long encounterId) {
        UserId = userId;
        EncounterId = encounterId;
    }

    public void Progress() {
        LastActivity = DateTime.UtcNow;
    }

    public void Complete() {
        SessionEnd = DateTime.UtcNow;
        Status = EncounterExecutionStatus.Completed;
    }
}