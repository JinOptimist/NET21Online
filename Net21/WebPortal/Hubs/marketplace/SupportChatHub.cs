using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace WebPortal.Hubs.marketplace
{
    public class SupportChatHub : Hub
    {
        public Task SendMessage(string user, string message)
        {
            /*
             Есть догадка почему может быть плохо 
            Мне кажется надо бы лучше добавить await вместо return и сделатть async Task
            Потому что так яя буду уверен что поток будет свободен, а при таком подходе я не уверен что поток будет свободен
            а так мы возвращаем заадачу без ожидания


            Как лучше ?)
             */
            return Clients.All.SendAsync("ReceiveMessage", user, message, System.DateTime.Now);
        }
    }
}