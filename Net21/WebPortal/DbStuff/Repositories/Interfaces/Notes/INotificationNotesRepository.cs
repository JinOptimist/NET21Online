using WebPortal.DbStuff.Models.Notes;

namespace WebPortal.DbStuff.Repositories.Interfaces.Notes;

public interface INotificationNotesRepository : IBaseDbRepository<NotificationNotes>
{
    NotificationNotes GetByIdWithUsers(int notificationId);
    List<NotificationNotes> GetNewNotificationForMe(int userId);
}