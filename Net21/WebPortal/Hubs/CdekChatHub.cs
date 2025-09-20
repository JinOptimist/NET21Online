using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using WebPortal.DbStuff.Repositories.Interfaces;
using WebPortal.Hubs.Interfaces;
using WebPortal.Services;

namespace WebPortal.Hubs
{
    public class CdekChatHub : Hub<ICdekChatClientHub>
    {
        private readonly IAuthService _authService;
        private readonly ICdekChatRepository _cdekChatRepository;

        public CdekChatHub(IAuthService authService, ICdekChatRepository cdekChatRepository)
        {
            _authService = authService;
            _cdekChatRepository = cdekChatRepository;
        }

        public override Task OnConnectedAsync()
        {
            if (_authService.IsAuthenticated())
            {
                var userId = _authService.GetId();
                _cdekChatRepository
                    .GetNewMessagesForMe(userId)
                    .ForEach(m =>
                    {
                        Clients.Caller.ReceiveMessage(m.Id, m.Author?.UserName ?? "System", m.Message);
                    });
            }

            return base.OnConnectedAsync();
        }

        // Опционально: если хочешь, чтобы клиент отправлял прямо в хаб
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.ReceiveMessage(0, user, message);
        }
    }
}