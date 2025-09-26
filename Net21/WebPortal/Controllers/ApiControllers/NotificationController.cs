using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebPortal.Controllers.CustomAuthorizeAttributes;
using WebPortal.DbStuff.Models.Notifications;
using WebPortal.DbStuff.Repositories.Interfaces;
using WebPortal.Enum;
using WebPortal.Hubs;
using WebPortal.Services;

namespace WebPortal.Controllers.ApiControllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]

    public class NotificationController : ControllerBase
    {
        private IHubContext<NotificationHub, INotificationHub> _notificationHub;
        private IAuthService _authService;
        private INotificationRepository _notificationRepository;

        public NotificationController(IHubContext<NotificationHub, INotificationHub> notificationHub,
            INotificationRepository notificationRepository,
            IAuthService authService)
        {
            _notificationHub = notificationHub;
            _notificationRepository = notificationRepository;
            _authService = authService;
        }

        [Role(Enum.Role.Admin)]
        public bool SendMessageToAll([FromForm] string message)
        {
            var user = _authService.GetUser();
            var notitication = new Notification
            {
                CreateAt = DateTime.Now,
                Message = message,
                Author = user,
                LevelNotification = null
            };
            _notificationRepository.Add(notitication);

            _notificationHub.Clients.All
                .NewNotification(notitication.Id, message)
                .Wait();

            return true;
        }

        public void ViewedByMe(int notificationId)
        {
            if (!_authService.IsAuthenticated())
            {
                return;
            }

            var user = _authService.GetUser();
            var notificaton = _notificationRepository
                .GetByIdWithUsers(notificationId);
            notificaton.UserWhoViewedIt.Add(user);
            _notificationRepository.Update(notificaton);
        }
    }
}
