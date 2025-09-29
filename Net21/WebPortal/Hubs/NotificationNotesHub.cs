using Microsoft.AspNetCore.SignalR;
using WebPortal.DbStuff.Repositories.Interfaces.Notes;
using WebPortal.Services;

namespace WebPortal.Hubs;

public class NotificationNotesHub : Hub<INotificationNotesHub>
{
    private IAuthService _authNotesService;
    private INotificationNotesRepository _notificationNotesRepository;

    public NotificationNotesHub(IAuthService authNotesService, INotificationNotesRepository notificationNotesRepository)
    {
        _authNotesService = authNotesService;
        _notificationNotesRepository = notificationNotesRepository;
    }
    
    public override Task OnConnectedAsync()
    {
        if (_authNotesService.IsAuthenticated())
        {
            var userId = _authNotesService.GetId();
            _notificationNotesRepository
                .GetNewNotificationForMe(userId)
                .ForEach(notification =>
                {
                    Clients.Caller.NewNotification(notification.Id, notification.Message);
                });
        }

        return base.OnConnectedAsync();
    }
}

public interface INotificationNotesHub
{
    Task NewNotification(int id, string message);
}