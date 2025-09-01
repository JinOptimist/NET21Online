using WebPortal.DbStuff.Models.Notes;
using WebPortal.DbStuff.Repositories.Interfaces.Notes;
using WebPortal.Enum;

namespace WebPortal.Services;

public class AuthNotesService
{
    private IHttpContextAccessor _contextAccessor;
    private IUserNotesRepository _userNotesRepository;

    public AuthNotesService(
        IHttpContextAccessor contextAccessor, 
        IUserNotesRepository userRepository)
    {
        _contextAccessor = contextAccessor;
        _userNotesRepository = userRepository;
    }

    public int GetId()
    {
        var httpContext = _contextAccessor.HttpContext;
        if (httpContext?.User?.Identity?.IsAuthenticated != true)
        {
            return 0;
        }

        var claim = httpContext
            .User
            .Claims
            .FirstOrDefault(x => x.Type == "Id");

        return claim != null ? int.Parse(claim.Value) : 0;
    }

    public User GetUser()
    {
        var id = GetId();
        if (id == 0)
        {
            return new User
            {
                Id = 0,
                UserName = "Guest",
                AvatarUrl = "images/notes/avatars/guest.png"
            };
        }

        var user = _userNotesRepository.GetFirstById(id);

        return user;
    }

    public bool IsAuthenticated()
    {
        return _contextAccessor.HttpContext!.User?.Identity?.IsAuthenticated ?? false;
    }
    
    internal NotesUserRole GetRole()
    {
        var httpContext = _contextAccessor.HttpContext;
        return (NotesUserRole)int.Parse(httpContext
            .User
            .Claims
            .First(x => x.Type == "Role")
            .Value);
    }
}