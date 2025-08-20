using WebPortal.DbStuff.Models.Notes;
using WebPortal.DbStuff.Repositories.Interfaces.Notes;

namespace WebPortal.DbStuff.Repositories.Notes;

public class UserNotesRepository : BaseDbRepository<User>, IUserNotesRepository
{
    public UserNotesRepository(NotesDbContext portalContext) : base(portalContext)
    {
    }
}