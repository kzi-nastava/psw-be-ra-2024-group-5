using Explorer.Encounters.API.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.API.Dtos;
public class SocialEncounterDto : EncounterDto {
    public float Radius { get; set; }
    public int PeopleCount { get; set; }

    public SocialEncounterDto() {
        Type = EncounterType.Social;
    }
}
