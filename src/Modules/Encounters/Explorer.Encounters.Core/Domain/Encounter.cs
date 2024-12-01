using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Encounters.API.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.Domain;

public class Encounter : Entity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Location Location { get; set; }
    public int XP { get; set; }
    public EncounterStatus Status { get; private set; }
    public EncounterType Type { get; set; }
    public long CreatorId { get; set; }

    public Encounter() { }

    public Encounter(long id, string name, string description, Location location, int xp, EncounterStatus status, EncounterType type, long creatorId) {
        Id = id;
        Name = name;
        Description = description;
        Location = location;
        XP = xp;
        Status = status;
        Type = type;
        CreatorId = creatorId;
    }

    public void UpdateStatus(EncounterStatus status) {
        Status = status;
    }

    public bool IsClose(Location location, double radius = 50) {
        return GeoCalculator.IsClose(location, Location, radius);
    }
}
