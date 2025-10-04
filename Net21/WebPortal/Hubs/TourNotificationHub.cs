using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using static WebPortal.Hubs.TourNotificationHub;

namespace WebPortal.Hubs
{
    public class TourNotificationHub : Hub<ITourNotificationHub>
    {
        public void NotifyAllAboutTourCreation(string tourName, string autorName)
        {
            Clients.All
                .NewTourNotification(tourName, autorName)
                .Wait();

        }
    }

    public interface ITourNotificationHub
    {
        Task NewTourNotification(string tourName, string autorName);
    }
}
