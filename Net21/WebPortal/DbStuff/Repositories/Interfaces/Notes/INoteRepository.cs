using WebPortal.DbStuff.Models.Notes;

namespace WebPortal.DbStuff.Repositories.Interfaces.Notes;

public interface INoteRepository : IBaseDbRepository<Note>
{
    IEnumerable<Note> GetNotesLastWeek();
    IEnumerable<Note>GetNotesByCategoryAsync(int categoryId);
    IEnumerable<Note> GetNotesByTagsAsync(IEnumerable<int> tagIds);
    IEnumerable<Note> GetAllWithAuthor();
    Note GetNoteWithTags(int id);
}