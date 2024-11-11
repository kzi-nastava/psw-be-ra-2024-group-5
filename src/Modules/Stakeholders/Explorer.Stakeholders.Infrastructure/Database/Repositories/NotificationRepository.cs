using System;
using System.Collections.Generic;
using System.Linq;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories
{
    public class NotificationRepository : CrudDatabaseRepository<Notification, StakeholdersContext>, INotificationRepository
    {
        public NotificationRepository(StakeholdersContext context) : base(context)
        {
        }

        public PagedResult<Notification> GetPagedNotifications(long userId, int page, int pageSize)
        {
            var query = DbContext.Notifications
                .Include(n => n.UserReadStatuses)  
                .Where(n => n.UserReadStatuses.Any(r => r.UserId == userId));

            var totalItems = query.Count();
            var notifications = query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PagedResult<Notification>(notifications, totalItems);
        }


        public void MarkAsRead(long notificationId, long userId)
        {
            var notification = DbContext.Notifications.Include(n => n.UserReadStatuses)
                                                       .FirstOrDefault(n => n.Id == notificationId);

            if (notification != null)
            {
                var status = notification.UserReadStatuses.FirstOrDefault(r => r.UserId == userId);

                if (status != null && !status.IsRead)
                {
                    status.IsRead = true;
                    DbContext.SaveChanges();
                }
            }
        }

        public void MarkAllAsRead(long userId)
        {
            var unreadStatuses = DbContext.NotificationReadStatuses
                                          .Where(nrs => nrs.UserId == userId && !nrs.IsRead)
                                          .ToList();

            foreach (var status in unreadStatuses)
            {
                status.IsRead = true;
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
