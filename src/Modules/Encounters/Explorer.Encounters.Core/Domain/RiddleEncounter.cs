using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.Domain;
public class RiddleEncounter : Encounter {
    public string Riddle { get; set; } = string.Empty;
    public string Answer { get; set; } = string.Empty;
    public ICollection<string> PotentialAnswers { get; set; } = new List<string>();

    public RiddleEncounter() {
        Riddle = string.Empty;
        Answer = string.Empty;
        PotentialAnswers = new List<string>();
    }

    public RiddleEncounter(string riddle, string answer, List<string> potentialAnswers) {
        Riddle = riddle;
        Answer = answer;
        PotentialAnswers = potentialAnswers;
    }

    public bool CheckAnswer(string answer) {
        return Answer.Equals(answer, StringComparison.OrdinalIgnoreCase);
    }
}
