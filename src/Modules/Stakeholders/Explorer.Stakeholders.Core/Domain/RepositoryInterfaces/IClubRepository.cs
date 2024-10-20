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
        Result DeleteMembership(long clubId, long userId);
        Result CreateMembership(long clubId, long userId);
    }
}
