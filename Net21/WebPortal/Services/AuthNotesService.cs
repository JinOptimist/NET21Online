using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using WebPortal.Controllers;
using WebPortal.DbStuff.Models.Notes;
using WebPortal.DbStuff.Repositories.Interfaces.Notes;
using WebPortal.Enum;

namespace WebPortal.Services;

public class AuthNotesService : IAuthNotesService
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

    public async Task SignInUser(User user)
    {
        var claims = new List<Claim>
        {
            new Claim("Id", user.Id.ToString()),
            new Claim("Name", user.UserName),
            new Claim("Avatar", user.AvatarUrl ?? ""),
            new Claim("Role", ((int)user.Role).ToString()),
            new Claim("Language", ((int)user.Language).ToString()),
            new Claim(ClaimTypes.AuthenticationMethod, AuthNotesController.AUTH_KEY),
        };

        var identity = new ClaimsIdentity(claims, AuthNotesController.AUTH_KEY);
        var principal = new ClaimsPrincipal(identity);

        await _contextAccessor.HttpContext.SignInAsync(principal);
    }

    public NotesUserRole GetRole()
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
    
    public bool IsAdmin()
    {
        return IsAuthenticated() && GetRole() == NotesUserRole.Administrator;
    }
}