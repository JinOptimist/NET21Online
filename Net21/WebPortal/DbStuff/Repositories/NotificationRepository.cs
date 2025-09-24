using Microsoft.EntityFrameworkCore;
using WebPortal.DbStuff.Models.Notifications;
using WebPortal.DbStuff.Repositories.Interfaces;
using WebPortal.Enum;
using WebPortal.Services;

namespace WebPortal.DbStuff.Repositories
{
    public class NotificationRepository : BaseRepository<Notification>, INotificationRepository
    {
        private readonly IAuthService _authService;
        public NotificationRepository(WebPortalContext portalContext, IAuthService authService) : base(portalContext) 
        {
            _authService = authService;
        }

        public Notification GetByIdWithUsers(int notificationId)
        {
            return _dbSet
                .Include(x => x.UserWhoViewedIt)
                .First(x => x.Id == notificationId);
        }

        public List<Notification> GetNewNotificationForMe(int userId)
        {
            if (!_authService.IsAuthenticated())
            {
                throw new ArgumentException("User not a register", nameof(userId));
            }

            var userRole = _authService.GetRole();

            var lastWeek = DateTime.Now.AddDays(-7);
            return _dbSet
                .Where(notification =>
                    !notification.UserWhoViewedIt.Select(u => u.Id).Contains(userId)
                    && notification.CreateAt > lastWeek

                    && (notification.LevelNotification == null 
                    || notification.LevelNotification == userRole))
                .ToList();
        }
    }
}
