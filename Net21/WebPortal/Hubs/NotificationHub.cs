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

        public override Task OnConnectedAsync()
        {
            if (_authService.IsAuthenticated())
            {
                var userId = _authService.GetId();
                var userRole = _authService.GetRole();

                var notifications = _notificationRepository
                    .GetNewNotificationForMe(userId)
                    .ToList();

                foreach (var notification in notifications)
                {
                    Clients.Caller.NewNotification(notification.Id, notification.Message);
                }
            }

            return base.OnConnectedAsync();
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
