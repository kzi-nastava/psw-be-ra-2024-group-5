using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories
{
    public class NotificationRepository: CrudDatabaseRepository<Notification, StakeholdersContext>, INotificationRepository
    {
        public NotificationRepository(StakeholdersContext context): base(context)
        {

        }

        public PagedResult<Notification> GetPagedNotifications(long userId, int page, int pageSize)
        {
            var query = DbContext.Notifications.Where(n => n.UserId == userId);

            var totalItems = query.Count();
            var notifications = query
                                .Skip((page - 1) * pageSize)
                                .Take(pageSize)
                                .ToList();

            return new PagedResult<Notification>(notifications, totalItems);
        }

        public void MarkAsRead(long notificationId)
        {
            var notification = DbContext.Notifications.Find(notificationId);
            if (notification != null && !notification.IsRead)
            {
                notification.IsRead = true;
                DbContext.SaveChanges();
            }
        }

        public void MarkAllAsRead(long userId)
        {
            var unreadNotifications = DbContext.Notifications
                                              .Where(n => n.UserId == userId && !n.IsRead)
                                              .ToList();

            foreach (var notification in unreadNotifications)
            {
                notification.IsRead = true;
            }

            DbContext.SaveChanges();
        }

        public void Add(Notification notification)
        {
            DbContext.Notifications.Add(notification);  
            DbContext.SaveChanges();
        }
    }
}
