using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using WebPortal.DbStuff.Repositories.Interfaces;
using WebPortal.DbStuff.Repositories.Interfaces.Notes;
using WebPortal.Services;

namespace WebPortal.Hubs
{
    public class NotificationHub : Hub<INotificationHub>
    {
        private IAuthService _authService;
        private INotificationRepository _notificationRepository;

        public NotificationHub(IAuthService authService,
            INotificationRepository notificationRepository)
        {
            _authService = authService;
            _notificationRepository = notificationRepository;
        }

        public override async Task OnConnectedAsync()
        {
            if (!_authService.IsAuthenticated())
            {
                await base.OnConnectedAsync();
                return;
            }

            var userId = _authService.GetId();
            var userRole = _authService.GetRole();

            var notifications = _notificationRepository
                .GetNewNotificationForMe(userId)
                .Where(n => n.LevelNotification == null || n.LevelNotification == userRole)
                .ToList();

            foreach (var notification in notifications)
            {
                await Clients.Caller.NewNotification(notification.Id, notification.Message);
            }

            if (userRole == Enum.Role.Admin)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, "Admins");
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            if (_authService.IsAuthenticated() && _authService.GetRole() == Enum.Role.Admin)
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, "Admins");
            }

            await base.OnDisconnectedAsync(exception);
        }

        //public void NotifyAll(string message)
        //{
        //    var userName = _authService.IsAuthenticated()
        //        ? _authService.GetName()
        //        : "Guess";

        //    Clients.All
        //        .NewNotification($"{userName}: {message}")//.SendAsync("NewNotification", message)
        //        .Wait();
        //}
    }

    public interface INotificationHub
    {
        Task NewNotification(int id, string message);
    }
}
