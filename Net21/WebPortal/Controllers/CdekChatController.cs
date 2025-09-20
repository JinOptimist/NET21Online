using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebPortal.Controllers.CustomAuthorizeAttributes;
using WebPortal.DbStuff.Repositories.Interfaces;
using WebPortal.Hubs;
using WebPortal.Services;
using WebPortal.DbStuff.Models;
using System;
using WebPortal.Hubs.Interfaces;

namespace WebPortal.Controllers.ApiControllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CdekChatController : ControllerBase
    {
        private readonly IHubContext<CdekChatHub, ICdekChatClientHub> _chatHub;
        private readonly IAuthService _authService;
        private readonly ICdekChatRepository _cdekChatRepository;

        public CdekChatController(IHubContext<CdekChatHub, ICdekChatClientHub> chatHub,
            ICdekChatRepository cdekChatRepository,
            IAuthService authService)
        {
            _chatHub = chatHub;
            _cdekChatRepository = cdekChatRepository;
            _authService = authService;
        }

        [Role(Enum.Role.Admin)]
        public bool SendMessageToAll([FromForm] string message)
        {
            var user = _authService.GetUser();
            var chatMessage = new CdekChat
            {
                CreatedAt = DateTime.Now,
                Message = message,
                Author = user,
            };

            _cdekChatRepository.Add(chatMessage);

            // Вызов типизированного хаба
            _chatHub.Clients.All
                .ReceiveMessage(chatMessage.Id, user?.UserName ?? "Admin", message)
                .Wait();

            return true;
        }

        public void ViewedByMe(int cdekChatId)
        {
            if (!_authService.IsAuthenticated()) return;

            var user = _authService.GetUser();
            var chatMessage = _cdekChatRepository.GetByIdWithUsers(cdekChatId);
            chatMessage.UserWhoViewedIt.Add(user);
            _cdekChatRepository.Update(chatMessage);
        }
    }
}