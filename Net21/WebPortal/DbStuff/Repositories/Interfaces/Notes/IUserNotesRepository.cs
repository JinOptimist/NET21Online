using WebPortal.DbStuff.Models.Notes;

namespace WebPortal.DbStuff.Repositories.Interfaces.Notes;

public interface IUserNotesRepository : IBaseDbRepository<User>
{
    bool IsUniqUserName(string userName);
    User? Login(string userName, string password);
    User Registration(string userName, string password);
}