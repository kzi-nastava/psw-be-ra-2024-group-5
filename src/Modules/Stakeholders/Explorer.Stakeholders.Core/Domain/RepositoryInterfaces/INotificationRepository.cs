using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.UseCases;

namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces
{
    public interface INotificationRepository: ICrudRepository<Notification>
    {
        PagedResult<Notification> GetPagedNotifications(long userId, int page, int pageSize);
        void MarkAsRead(long notificationId, long userId);
        void MarkAllAsRead(long userId);
        void Add(Notification notification);
    }
}
