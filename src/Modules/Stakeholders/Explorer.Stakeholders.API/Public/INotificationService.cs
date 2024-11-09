using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using FluentResults;
using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Stakeholders.API.Public
{
    public interface INotificationService
    {
        Result<PagedResult<NotificationDto>> GetPagedNotifications(long userId, int page, int pageSize);
        Result MarkNotificationAsRead(long notificationId);
        Result MarkAllNotificationsAsRead(long userId);
        Result<NotificationDto> SendNotification(long userId, NotificationDto notificationDto);
    }
}
