using Microsoft.AspNetCore.SignalR;

namespace WebPortal.Hubs;

public class CdekChatHub : Hub
{
    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }
}