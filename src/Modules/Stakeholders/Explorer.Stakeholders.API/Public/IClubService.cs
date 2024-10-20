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
    public interface IClubService
    {
        Result<PagedResult<ClubDto>> GetPaged(int page, int pageSize);
        Result<ClubDto> Create(ClubDto club);
        Result<ClubDto> Update(ClubDto club);
        Result Delete(int id);
        Result DeleteMembership(long clubId, long userId);
        Result CreateMembership(long clubId, long userId);

    }
}
