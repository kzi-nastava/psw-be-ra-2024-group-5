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

        public Result AddMessageToClub(long clubId, ClubMessage message, long userId)
        {
            var club = _clubRepository.Get(clubId);
            if (club == null)
                return Result.Fail("Club not found");

            var memberships = _clubRepository.GetAllMemberships();
            var membership = memberships.FirstOrDefault(m => m.UserId == userId && m.ClubId == clubId);

            if(membership != null || club.OwnerId == userId)
            {
                club.AddMessage(message);
                _clubRepository.Update(club);
                return Result.Ok();
            } else
            {
                return Result.Fail("User does not have permission to add messages to this club");
            }
        }

        public Result RemoveMessageFromClub(long clubId, long messageId, long userId) 
        {
            var club = _clubRepository.Get(clubId);
            if (club == null)
                return Result.Fail("Club not found");

            var message = club.GetClubMessages().FirstOrDefault(m => m.Id == messageId);
            if (message == null)
                return Result.Fail("Message not found");

            if (club.OwnerId == userId)
            {
                club.RemoveMessage(messageId);
                _clubRepository.Update(club);
                return Result.Ok();
            } else
            {
                return Result.Fail("User does not have permission to remove messages from this club");
            }
        }

        public Result UpdateMessageInClub(long clubId, long messageId, long userId, string newContent)
        {
            var club = _clubRepository.Get(clubId);
            if (club == null)
                return Result.Fail("Club not found");

            var message = club.GetClubMessages().FirstOrDefault(m => m.Id == messageId);
            if (message == null)
                return Result.Fail("Message not found");

            if(message.SenderId == userId)
            {
                club.UpdateMessage(messageId, newContent);
                _clubRepository.Update(club);
                return Result.Ok();
            } else
            {
                return Result.Fail("User does not have permission to update this message");
            }
        }
    }
}
