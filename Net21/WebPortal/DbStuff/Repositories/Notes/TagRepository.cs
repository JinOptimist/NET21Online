using WebPortal.DbStuff.Models.Notes;
using WebPortal.DbStuff.Repositories.Interfaces.Notes;

namespace WebPortal.DbStuff.Repositories.Notes;

public class TagRepository : BaseDbRepository<Tag>, ITagRepository
{
    public TagRepository(NotesDbContext notesDbContext) : base(notesDbContext)
    {
    }
    
    public List<string> GetTagNamesByIds(List<int> tagIds)
    {
        return _dbSet
            .Where(t => tagIds.Contains(t.Id))
            .Select(t => t.Name)
            .ToList();
    }
}