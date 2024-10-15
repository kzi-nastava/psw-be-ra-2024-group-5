using AutoMapper;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.Core.UseCases
{
    public class ClubMembershipService : BaseService<ClubMembershipDto,ClubMembership>,IClubMembershipService
    {
        public ClubMembershipService(IMapper mapper) : base(mapper) { }
        //public Result<ClubDto> AddMember(ClubDto club, User user)
        //{
        //    club.Members.Add(user);
        //    return club;
        //}

        //public Result<ClubDto> RemoveMember(ClubDto club, User user)
        //{
        //    club.Members.Remove(user);
        //    return club;
        //}
    }
}
