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
