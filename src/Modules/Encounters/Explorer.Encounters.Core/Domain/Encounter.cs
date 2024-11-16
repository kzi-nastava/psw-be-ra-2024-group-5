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
    public Position Location { get; set; }
    public int XP { get; set; }
    public EncounterStatus Status { get; set; }
    public EncounterType Type { get; set; }
    public long CreatorId { get; set; }

    public Encounter(string name, string description, Position location, int xp, EncounterStatus status, EncounterType type, long creatorId) 
    {
        Name = name;
        Description = description;
        Location = location;
        XP = xp;
        Status = status;
        Type = type;
        CreatorId = creatorId;
    }
}
