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
    public class ClubMembershipService : CrudService<ClubMembershipDto,ClubMembership>,IClubMembershipService
    {
        public ClubMembershipService(ICrudRepository<ClubMembership> crudRepository, IMapper mapper) : base(crudRepository, mapper)
        {
        }
    }
}
