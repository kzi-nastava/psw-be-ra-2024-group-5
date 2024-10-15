using Explorer.Blog.API.Dtos;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.Core.Domain;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.API.Public
{
    public interface IClubService
    {
        Result<PagedResult<ClubDto>> GetPaged(int page, int pageSize);
        Result<ClubDto> Create(ClubDto club);
        Result<ClubDto> Update(ClubDto club);
        Result Delete(int id);
        Result<ClubDto> AddMember(ClubDto club,User user);
        Result<ClubDto> RemoveMember(ClubDto club,User user);

    }
}
