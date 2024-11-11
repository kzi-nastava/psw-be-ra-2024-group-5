using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class NotificationService: BaseService<NotificationDto, Notification>, INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IMapper _mapper;

        public NotificationService(INotificationRepository notificationRepository, IMapper mapper) : base(mapper)
        {
            _notificationRepository = notificationRepository;
            _mapper = mapper;
        }

        public Result<PagedResult<NotificationDto>> GetPagedNotifications(long userId, int page, int pageSize)
        {
            var pagedNotifications = _notificationRepository.GetPagedNotifications(userId, page, pageSize);

            var pagedNotificationDtos = new PagedResult<NotificationDto>(
                _mapper.Map<List<NotificationDto>>(pagedNotifications.Results),
                pagedNotifications.TotalCount
            );

            return Result.Ok(pagedNotificationDtos);
        }

        public Result MarkNotificationAsRead(long notificationId, long userId)
        {
            _notificationRepository.MarkAsRead(notificationId, userId);
            return Result.Ok();
        }

        public Result MarkAllNotificationsAsRead(long userId)
        {
            _notificationRepository.MarkAllAsRead(userId);
            return Result.Ok();
        }

        public Result<NotificationDto> SendNotification(NotificationDto notificationDto)
        {
            var notification = MapToDomain(notificationDto);

            _notificationRepository.Add(notification);

            var notificationResultDto = _mapper.Map<NotificationDto>(notification);

            return Result.Ok(notificationResultDto);
        }
    }
}
