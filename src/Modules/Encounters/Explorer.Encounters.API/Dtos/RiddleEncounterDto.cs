using Explorer.Encounters.API.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.API.Dtos;
public class RiddleEncounterDto : EncounterDto {
    public string Riddle { get; set; }
    public ICollection<string> PotentialAnswers { get; set; } = new List<string>();
    public RiddleEncounterDto() {
        Type = EncounterType.Riddle;
    }
}
