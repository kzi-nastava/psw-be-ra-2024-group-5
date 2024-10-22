using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain
{
    public class ClubMembership
    {
        public long ClubId { get; private set; }
        public long UserId { get; private set; }
        public ClubMembership(long clubId, long userId) 
        {
            ClubId = clubId;
            UserId = userId;
        }

    }
}
