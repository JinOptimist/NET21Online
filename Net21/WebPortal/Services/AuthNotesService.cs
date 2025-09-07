using WebPortal.DbStuff.Models.Notes;
using WebPortal.DbStuff.Repositories.Interfaces.Notes;
using WebPortal.Enum;

namespace WebPortal.Services;

public class AuthNotesService : IAuthService, ILanguageService
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
        return int.Parse(httpContext
            .User
            .Claims
            .First(x => x.Type == "Id")
            .Value);
    }

    public User GetUser()
    {
        return _userNotesRepository.GetFirstById(GetId());
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

    public string GetName()
    {
        var httpContext = _contextAccessor.HttpContext;
        return httpContext
            .User
            .Claims
            .First(x => x.Type == "Name")
            .Value;
    }

    public Language GetLanguage()
    {
        return (Language)int.Parse(_contextAccessor.HttpContext
            .User
            .Claims
            .First(x => x.Type == "Language")
            .Value);
    }
}