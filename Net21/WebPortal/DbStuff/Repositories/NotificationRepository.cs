using Microsoft.EntityFrameworkCore;
using WebPortal.DbStuff.Models.Notifications;
using WebPortal.DbStuff.Repositories.Interfaces;

namespace WebPortal.DbStuff.Repositories
{
    public class NotificationRepository : BaseRepository<Notification>, INotificationRepository
    {
        public NotificationRepository(WebPortalContext portalContext) : base(portalContext)
        {
        }

        public Notification GetByIdWithUsers(int notificationId)
        {
            return _dbSet
                .Include(x => x.UserWhoViewedIt)
                .First(x => x.Id == notificationId);
        }

        public List<Notification> GetNewNotificationForMe(int userId)
        {
            var lastWeek = DateTime.Now.AddDays(-7);
            return _dbSet
                .Where(notification =>
                    !notification.UserWhoViewedIt.Select(u => u.Id).Contains(userId)
                    && notification.CreateAt > lastWeek)
                .ToList();
        }
    }
}
