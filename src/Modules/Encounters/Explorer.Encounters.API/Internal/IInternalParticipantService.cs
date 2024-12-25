using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.API.Internal
{
    public interface IInternalParticipantService
    {
        void AddXP(long userId, int xp);
    }
}
