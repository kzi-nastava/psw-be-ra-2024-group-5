using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.API.Dtos
{
    public class ParticipantDto
    {
        public long UserId { get; set; }
        public int XP { get; set; }
        public int Level { get; set; }

        public ParticipantDto(long userId, int xp, int level)
        {
            UserId = userId;
            XP = xp;
            Level = level;
        }
    }
}
