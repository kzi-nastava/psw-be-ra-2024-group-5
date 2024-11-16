using Explorer.Encounters.API.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.API.Dtos;

public class EncounterDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public LocationDto Location { get; set; }
    public int XP { get; set; }
    public EncounterStatus Status { get; set; }
    public EncounterType Type { get; set; }
    public long CreatorId { get; set; }

    // Dodati DTO za Social i Hidden encounter
}
