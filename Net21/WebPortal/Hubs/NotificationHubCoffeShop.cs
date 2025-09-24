using Microsoft.AspNetCore.SignalR;
using WebPortal.Services;

namespace WebPortal.Hubs
{
    public class NotificationHubCoffeShop : Hub<INotificationHubCoffeShop>
    {
        private IAuthService _authService;

        public NotificationHubCoffeShop(IAuthService authService)
        {
            _authService = authService;
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }
        
        public void NotifyAllCoffeShop(string message)
        {
            var userName = _authService.IsAuthenticated()
                ?
                _authService.GetName()
                : "Guest" ; 
       
            Clients.All
                .NewNotificationCoffeShop($"{userName}: {message}")
                //.SendAsync("NewNottificationCoffeShopAll", message)
                .Wait();


        }
    }

    public interface INotificationHubCoffeShop
    {
        Task NewNotificationCoffeShop(string message);
    }
}
