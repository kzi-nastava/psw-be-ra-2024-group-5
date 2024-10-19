using AutoMapper;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class ClubMembershipService : CrudService<ClubMembershipDto,ClubMembership>,IClubMembershipService
    {
        public ClubMembershipService(ICrudRepository<ClubMembership> crudRepository, IMapper mapper) : base(crudRepository, mapper)
        {
        }
    }
}
