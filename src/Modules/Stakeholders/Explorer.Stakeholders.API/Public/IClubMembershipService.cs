using Explorer.Stakeholders.API.Dtos;
using Explorer.BuildingBlocks.Core.UseCases;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Public
{
    public interface IClubMembershipService
    {
        Result<PagedResult<ClubMembershipDto>> GetPaged(int page, int pageSize);
        Result<ClubMembershipDto> Create(ClubMembershipDto clubMembership);
        Result Delete(int id);
    }
}
