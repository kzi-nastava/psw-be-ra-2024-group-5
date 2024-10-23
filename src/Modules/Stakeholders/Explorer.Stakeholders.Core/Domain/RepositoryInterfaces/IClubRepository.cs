using Explorer.BuildingBlocks.Core.UseCases;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces
{
    public interface IClubRepository : ICrudRepository<Club>
    {
        bool DeleteMembership(long clubId, long userId);
        ClubMembership? CreateMembership(long clubId, long userId);
        List<ClubMembership> GetAllMemberships();
    }
}
