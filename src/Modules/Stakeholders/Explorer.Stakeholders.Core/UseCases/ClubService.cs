using AutoMapper;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class ClubService : CrudService<ClubDto, Club>, IClubService
    {
        private readonly IClubRepository _clubRepository;
        public ClubService(IClubRepository clubRepository, IMapper mapper) : base(clubRepository, mapper)
        {
            _clubRepository = clubRepository;
        }
        //extremely unefficient,can't do MapToDto since BaseService only uses Club 
        public Result<List<ClubMembershipDto>> GetAllMemberships() 
        {
            List<ClubMembership> memberships = _clubRepository.GetAllMemberships();

            List<ClubMembershipDto> membershipsDto = new List<ClubMembershipDto>();

            foreach (var member in memberships) 
            {
                ClubMembershipDto memberDto = new ClubMembershipDto
                {
                    ClubId = member.ClubId,
                    UserId = member.UserId
                };
                membershipsDto.Add(memberDto);
            }

            Result<List<ClubMembershipDto>> result = membershipsDto;

            if (result.IsFailed) return Result.Fail(result.Errors);
            return Result.Ok(result.Value);
        }

        public Result CreateMembership(long clubId, long userId)
        {
            Result<ClubMembership?> result = _clubRepository.CreateMembership(clubId, userId);
            if (result != null) return Result.Ok();
            return Result.Fail("An error occurred while attempting to create the membership.");
        }

        public Result DeleteMembership(long clubId, long userId)
        {
            bool result = _clubRepository.DeleteMembership(clubId, userId);
            if (result) return Result.Ok();
            return Result.Fail("An error occurred while attempting to delete the membership.");
        }
    }
}
