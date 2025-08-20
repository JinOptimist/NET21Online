using WebPortal.DbStuff.Models.Notes;
using WebPortal.DbStuff.Repositories.Interfaces.Notes;

namespace WebPortal.DbStuff.Repositories.Notes;

public class UserRepository : BaseDbRepository<User>, IUserRepository
{
    public UserRepository(NotesDbContext portalContext) : base(portalContext)
    {
    }
}