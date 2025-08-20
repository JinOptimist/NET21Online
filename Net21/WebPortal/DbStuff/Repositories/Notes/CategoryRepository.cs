using WebPortal.DbStuff.Models.Notes;
using WebPortal.DbStuff.Repositories.Interfaces.Notes;

namespace WebPortal.DbStuff.Repositories.Notes;

public class CategoryRepository : BaseDbRepository<Category>, ICategoryRepository
{
    public CategoryRepository(NotesDbContext notesDbContext) : base(notesDbContext)
    {
    }
}