using Microsoft.AspNetCore.SignalR;

namespace WebPortal.Hubs
{
    public class NotificationHubCoffeShop : Hub<INotificationHubCoffeShop>
    {
        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }
        
        public void NotifyAllCoffeShop(string message)
        { 
       
            Clients.All
                .NewNotificationCoffeShop(message)
                //.SendAsync("NewNottificationCoffeShopAll", message)
                .Wait();


        }
    }

    public interface INotificationHubCoffeShop
    {
        Task NewNotificationCoffeShop(string message);
    }
}
