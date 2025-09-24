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
        
        [HttpPost]
        public bool SendMessageToUser([FromForm] string message)
        {
            // Получаем пользователя, который отправляет сообщение
            var sender = _authService.GetUser();
            
            // Создаём объект сообщения
            var chatMessage = new CdekChat
            {
                CreatedAt = DateTime.Now,
                Message = message,
                Author = sender
            };

            // Сохраняем сообщение в БД
            _cdekChatRepository.Add(chatMessage);

             // Отправка сообщения только конкретному пользователю
             _chatHub.Clients.All
                 .ReceiveMessage(chatMessage.Id, sender.UserName, message);
             
             return true;
        }
        
        public void ViewedByMe(int cdekChatId)
        {
            if (!_authService.IsAuthenticated())
            {
                return;
            }

            var user = _authService.GetUser();
            var chatMessage = _cdekChatRepository
                .GetByIdWithUsers(cdekChatId);
            chatMessage.UserWhoViewedIt.Add(user);
            _cdekChatRepository.Update(chatMessage);
        }
    }
}