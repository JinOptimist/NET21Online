using WebPortal.DbStuff.Models.Notes;
using WebPortal.DbStuff.Repositories.Interfaces.Notes;

namespace WebPortal.DbStuff.Repositories.Notes;

public class TagRepository : BaseDbRepository<Tag>, ITagRepository
{
    public TagRepository(NotesDbContext notesDbContext) : base(notesDbContext)
    {
    }
}