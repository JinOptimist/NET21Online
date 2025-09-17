using WebPortal.DbStuff.Models.Notifications;

namespace WebPortal.DbStuff.Repositories.Interfaces
{
    public interface INotificationRepository : IBaseRepository<Notification>
    {
        Notification GetByIdWithUsers(int notificationId);
        List<Notification> GetNewNotificationForMe(int userId);
    }
}
