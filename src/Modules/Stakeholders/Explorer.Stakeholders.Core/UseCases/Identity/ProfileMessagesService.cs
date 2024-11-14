using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Dtos.Messages;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.Messages;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Stakeholders.Core.UseCases.Identity
{
    public class ProfileMessagesService : IProfileMessagesService
    {
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly ICrudRepository<User> _userRepository;
        private readonly ICrudRepository<Person> _personRepository;
        private readonly ICrudRepository<Following> _followingRepository;
        private readonly IFollowingService _followingService;
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;

        public ProfileMessagesService(IUserProfileRepository userProfileRepository, ICrudRepository<User> userRepository,
            ICrudRepository<Person> personRepository, IFollowingService followingService, IMapper mapper, INotificationService notificationService)
        {
            _userProfileRepository = userProfileRepository;
            _userRepository = userRepository;
            _personRepository = personRepository;
            _followingService = followingService;
            _mapper = mapper;
            _notificationService = notificationService;
        }

        public Result<MessageDto> SendMessage(long senderId, long recipientId, string content, AttachmentDto? attachment)
        {
            try
            {
                if (senderId == recipientId)
                    return Result.Fail(FailureCode.InvalidArgument).WithError("Sender and recipient cannot be the same.");

                if (!_userProfileRepository.Exists(senderId) || !_userProfileRepository.Exists(recipientId))
                    return Result.Fail(FailureCode.NotFound).WithError("Sender or recipient not found.");

                if (!_followingService.IsAlreadyFollowing(recipientId, senderId))
                    return Result.Fail(FailureCode.Forbidden).WithError("Recipient is not following sender.");

                var message = new ProfileMessage(senderId, recipientId, content);

                if (attachment != null)
                {
                    var attachmentDomain = new Attachment(attachment.ResourceId, (ResourceType)attachment.ResourceType);
                    message.AddAttachment(attachmentDomain);
                }

                var recipientProfile = _userProfileRepository.Get(recipientId)!;

                recipientProfile.AddMessage(message);
                _userProfileRepository.Update(recipientProfile);

                SendNotification(senderId, recipientId, content, attachment);

                return Result.Ok(new MessageDto(-1, senderId, GetProfileDisplayName(senderId) ?? "",
                    message.Content, message.SentAt, message.IsRead, attachment));
            }
            catch (KeyNotFoundException)
            {
                return Result.Fail(FailureCode.NotFound).WithError("Sender or recipient not found.");
            }
        }

        private void SendNotification(long senderId, long recipientId, string content, AttachmentDto? attachment)
        {
            var notificationDto = new NotificationDto
            {
                Content = "You have a new message!",
                CreatedAt = DateTime.UtcNow,
                SenderId = senderId,
                UserIds =  new List<long> { recipientId },
                Type = 0,
                Message = content,
                Attachment = attachment,
                UserReadStatuses = new List<NotificationReadStatusDto>
                {
                    new NotificationReadStatusDto
                    {
                        UserId = recipientId,
                        NotificationId = 0,
                        IsRead = false
                    }
                }
            };

            _notificationService.SendNotification(notificationDto);
        }

        public Result ViewMessage(long profileId, long messageId)
        {
            try
            {
                var recipientProfile = _userProfileRepository.Get(profileId);

                if (recipientProfile == null)
                    return Result.Fail(FailureCode.NotFound).WithError($"Profile with id:{profileId} not found.");

                if (recipientProfile.ViewMessage(messageId))
                {
                    _userProfileRepository.Update(recipientProfile);
                    return Result.Ok();
                }
                else return Result.Fail(FailureCode.NotFound).WithError($"Message with id:{messageId} not found.");
            }
            catch (KeyNotFoundException)
            {
                return Result.Fail(FailureCode.NotFound).WithError($"Profile with id:{profileId} not found.");
            }
            catch (Exception e)
            {
                return Result.Fail(FailureCode.Internal).WithError(e.Message);
            }
        }

        private Person? GetPersonByUserId(long userId)
        {
            var pagedPersons = _personRepository.GetPaged(0, 0);
            return pagedPersons.Results.FirstOrDefault(person => person.UserId == userId);
        }

        private string GetProfileDisplayName(long profileId)
        {
            try
            {
                var userProfile = _userProfileRepository.Get(profileId);

                if (userProfile == null) return null;

                var person = GetPersonByUserId(userProfile.UserId);
                var displayName = person == null ? "" : $"{person.Name} {person.Surname}";

                return displayName;
            }
            catch (KeyNotFoundException)
            {
                return string.Empty;
            }
        }
    }
}
