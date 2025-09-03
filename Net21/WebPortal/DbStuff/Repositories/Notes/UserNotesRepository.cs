using WebPortal.DbStuff.Models.Notes;
using WebPortal.DbStuff.Repositories.Interfaces.Notes;
using WebPortal.Enum;
using WebPortal.Services;

namespace WebPortal.DbStuff.Repositories.Notes;

public class UserNotesRepository : BaseDbRepository<User>, IUserNotesRepository
{
    private readonly PasswordService _passwordService;

    public UserNotesRepository(
        NotesDbContext portalContext,
        PasswordService passwordService) : base(portalContext)
    {
        _passwordService = passwordService;
    }

    public override User Add(User model)
    {
        throw new Exception("DO NOT USER Add. User Registration method");
    }

    public bool IsUniqUserName(string? userName)
    {
        return !_dbSet.Any(x => x.UserName == userName);
    }

    public User? Login(string userName, string password)
    {
        var user = _dbSet.FirstOrDefault(x => x.UserName == userName);

        if (user == null)
        {
            return null;
        }

        return _passwordService.VerifyPassword(user, password, user.PasswordHash)
            ? user
            : null;
    }

    public void Registration(string userName, string password)
    {
        if (_dbSet.Any(x => x.UserName == userName))
        {
            throw new Exception($"{userName} already exist");
        }

        var user = new User
        {
            UserName = userName,
            AvatarUrl = "images/notes/avatars/default.png",
            Role = NotesUserRole.User
        };
        user.PasswordHash = _passwordService.HashPassword(user, password);

        _dbSet.Add(user);
        _portalContext.SaveChanges();
    }
}