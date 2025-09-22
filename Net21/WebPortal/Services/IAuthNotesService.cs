using WebPortal.DbStuff.Models.Notes;
using WebPortal.Enum;

namespace WebPortal.Services;

public interface IAuthNotesService
{
    int GetId();
    User GetUser();
    bool IsAuthenticated();
    Language GetLanguage();
    NotesUserRole GetRole();
    string GetName();
    bool IsAdmin();
    Task SignInUser(User user);
}