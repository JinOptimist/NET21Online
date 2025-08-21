using Microsoft.EntityFrameworkCore;
using WebPortal.DbStuff.Models.Notes;
using WebPortal.DbStuff.Repositories.Interfaces.Notes;

namespace WebPortal.DbStuff.Repositories.Notes;

public class NoteRepository : BaseDbRepository<Note>, INoteRepository
{
    public NoteRepository(NotesDbContext notesDbContext) : base(notesDbContext)
    {
    }

    public IEnumerable<Note> GetNotesLastWeek()
    {
        var lastWeek = DateTime.UtcNow.AddDays(-7);

        return _dbSet
            .Include(n => n.Category)
            .Include(n => n.Tags)
            .Include(n => n.Author)
            .Where(n => n.CreateDate >= lastWeek)
            .ToList();
    }

    public IEnumerable<Note> GetNotesByCategoryAsync(int categoryId)
    {
        return _dbSet
            .Include(n => n.Category)
            .Include(n => n.Tags)
            .Where(n => n.CategoryId == categoryId)
            .ToList();
    }

    public IEnumerable<Note> GetNotesByTagsAsync(IEnumerable<int> tagIds)
    {
        return _dbSet
            .Include(n => n.Category)
            .Include(n => n.Tags)
            .Where(n => n.Tags.Any(t => tagIds.Contains(t.Id)))
            .ToList();
    }
    
    public IEnumerable<Note> GetAllWithAuthor()
    {
        return _dbSet
            .Include(x => x.Author)
            .ToList();
    }
}