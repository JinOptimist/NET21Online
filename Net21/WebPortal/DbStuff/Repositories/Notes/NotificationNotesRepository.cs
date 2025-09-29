using Microsoft.EntityFrameworkCore;
using WebPortal.DbStuff.Models.Notes;
using WebPortal.DbStuff.Repositories.Interfaces.Notes;

namespace WebPortal.DbStuff.Repositories.Notes;

public class NotificationNotesRepository : BaseDbRepository<NotificationNotes>, INotificationNotesRepository
{
    public NotificationNotesRepository(NotesDbContext notesDbContext) : base(notesDbContext)
    {
    }

    public NotificationNotes GetByIdWithUsers(int notificationId)
    {
        return _dbSet
            .Include(x => x.UserWhoViewedIt)
            .First(x => x.Id == notificationId);
    }

    public List<NotificationNotes> GetNewNotificationForMe(int userId)
    {
        var lastWeek = DateTime.Now.AddDays(-7);
        return _dbSet
            .Where(notification =>
                !notification.UserWhoViewedIt.Select(u => u.Id).Contains(userId)
                && notification.CreateAt > lastWeek)
            .ToList();
    }
}