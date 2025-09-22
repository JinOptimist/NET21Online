using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebPortal.DbStuff.Models.Notes;
using WebPortal.DbStuff.Repositories.Interfaces.Notes;
using WebPortal.Hubs;
using WebPortal.Services;

namespace WebPortal.Controllers.ApiControllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class NotificationNotesController : ControllerBase
{
    private IHubContext<NotificationNotesHub, INotificationNotesHub> _notificationNotesHub;
    private IAuthNotesService _authNotesService;
    private INotificationNotesRepository _notificationNotesRepository;

    public NotificationNotesController(IHubContext<NotificationNotesHub, INotificationNotesHub> notificationNotesHub,
        IAuthNotesService authNotesService, INotificationNotesRepository notificationNotesRepository)
    {
        _notificationNotesHub = notificationNotesHub;
        _authNotesService = authNotesService;
        _notificationNotesRepository = notificationNotesRepository;
    }
    
    public bool SendMessageToAll([FromForm] string message)
    {
        var user = _authNotesService.GetUser();
        var notitication = new NotificationNotes
        {
            CreateAt = DateTime.Now,
            Message = message,
            Author = user,
        };
        _notificationNotesRepository.Add(notitication);

        _notificationNotesHub.Clients.All
            .NewNotification(notitication.Id, message)
            .Wait();
        return true;
    }
}