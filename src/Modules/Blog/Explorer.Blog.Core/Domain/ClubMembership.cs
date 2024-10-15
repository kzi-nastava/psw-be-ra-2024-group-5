using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.Core.Domain
{
    public class ClubMembership : Entity
    {
        public int ClubId { get; private set; }
        public int UserId { get; private set; }
        public ClubMembership() 
        {
            UserId = 0;
            ClubId = 0;
        }
        public ClubMembership(int clubId, int userId) 
        {
            ClubId = clubId;
            UserId = userId;
        }

    }
}
