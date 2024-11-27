using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.Domain;

public class Participant : Entity
{
    public long UserId { get; set; }
    public int XP { get; set; }
    public int Level { get; set; }
    public Participant() { }

    public Participant(long userId, int xp, int level) {
        UserId = userId;
        XP = xp;
        Level = level;
    }

    public void AddXP(int xp) {
        XP += xp;

        // Neka logika kada se levelupuje
    }

    public void LevelUp() {
        Level++;

        // logika za koliko levela se levelupuje
    }
}
