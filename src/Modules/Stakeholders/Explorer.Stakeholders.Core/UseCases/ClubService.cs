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
using Explorer.Stakeholders.Core.Domain.Messages;
using Explorer.Stakeholders.API.Dtos.Messages;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class ClubService : CrudService<ClubDto, Club>, IClubService
    {
        private readonly IClubRepository _clubRepository;
        private readonly ICrudRepository<ClubMessage> _clubMessageRepository;
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;
        public ClubService(IClubRepository clubRepository, IMapper mapper, 
            ICrudRepository<ClubMessage> clubMessageRepository, INotificationService notificationService) : base(clubRepository, mapper)
        {
            _clubRepository = clubRepository;
            _clubMessageRepository = clubMessageRepository;
            _mapper = mapper;
            _notificationService = notificationService;
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

        public Result AddMessageToClub(long clubId, ClubMessageDto messageDto, long userId)
        {
            var club = _clubRepository.Get(clubId);
            if (club == null)
                return Result.Fail("Club not found");

            var memberships = _clubRepository.GetAllMemberships();
            var membership = memberships.FirstOrDefault(m => m.UserId == userId && m.ClubId == clubId);

            if (membership != null || club.OwnerId == userId)
            {
                club.AddMessage(_mapper.Map<ClubMessage>(messageDto));
                _clubRepository.Update(club);

                sendNotificationForAdding(clubId, messageDto, userId, memberships);

                return Result.Ok();
            }
            else
            {
                return Result.Fail("User does not have permission to add messages to this club");
            }
        }

        private NotificationDto sendNotificationForAdding(long clubId, ClubMessageDto messageDto, long userId, List<ClubMembership> memberships)
        {
            var club = _clubRepository.Get(clubId);
            var notificationDto = new NotificationDto
            {
                Content = "A new message was posted in the club!",
                CreatedAt = DateTime.UtcNow,
                SenderId = userId,
                ClubId = clubId,
                UserIds = memberships
                        .Where(m => m.ClubId == clubId)
                        .Select(m => m.UserId)
                        .Where(UserId => UserId != userId)
                        .Concat(new List<long> { club.OwnerId })
                        .Where(ownerId => ownerId != userId)
                        .Distinct()
                        .ToList(),
                Type = 1,
                Message = messageDto.Content,
                Attachment = messageDto.Attachment,
                UserReadStatuses = memberships
                        .Where(m => m.ClubId == clubId)
                        .Select(m => m.UserId)
                        .Where(id => id != userId)
                        .Concat(new List<long> { club.OwnerId })
                        .Where(ownerId => ownerId != userId)
                        .Distinct()
                        .Select(userId => new NotificationReadStatusDto
                        {
                            UserId = userId,
                            NotificationId = 0,
                            IsRead = false
                        })
                        .ToList()
            };

            return _notificationService.SendNotification(notificationDto);
        }

        private NotificationDto SendNotificationForDeleting(long clubId, Message message, long userId, List<ClubMembership> memberships)
        {
            var club = _clubRepository.Get(clubId);
            var notificationDto = new NotificationDto
            {
                Content = "Your message was deleted from the club!",
                CreatedAt = DateTime.UtcNow,
                ClubId = clubId,
                UserIds = new List<long> { message.SenderId },
                Type = 2,
                Message = message.Content,
                Attachment = _mapper.Map<AttachmentDto>(message.Attachment),
                UserReadStatuses = new List<NotificationReadStatusDto>
                {
                    new NotificationReadStatusDto
                    {
                        UserId = message.SenderId,
                        NotificationId = 0,
                        IsRead = false
                    }
                }
            };

            return _notificationService.SendNotification(notificationDto);
        }

        public Result RemoveMessageFromClub(long clubId, long messageId, long userId) 
        {
            var club = _clubRepository.Get(clubId);
            if (club == null)
                return Result.Fail("Club not found");

            var message = _clubMessageRepository.Get(messageId);
            var messagePom = message;
            if (message == null)
                return Result.Fail("Message not found");

            if (club.OwnerId == userId)
            {
                club.RemoveMessage(messageId);
                _clubRepository.Update(club);

                var memberships = _clubRepository.GetAllMemberships();
                SendNotificationForDeleting(clubId, messagePom, userId, memberships);
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

            var message = _clubMessageRepository.Get(messageId);
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

        public Result<PagedResult<ClubMessageDto>> GetPagedMessagesByClubId(long clubId, int page, int pageSize)
        {
            var club = _clubRepository.Get(clubId);
            if (club == null)
            {
                return Result.Fail("Club not found");
            }

            var pagedMessages = _clubMessageRepository.GetPaged(page, pageSize);

            var filteredMessages = pagedMessages.Results
                .Where(m => m.ClubId == clubId)
                .ToList();

            var totalMessages = filteredMessages.Count;

            var pagedResultDto = new PagedResult<ClubMessageDto>(
                filteredMessages.Select(m => _mapper.Map<ClubMessageDto>(m)).ToList(),
                totalMessages
            );

            return Result.Ok(pagedResultDto);
        }

    }
}
