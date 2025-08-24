using WebPortal.DbStuff.Models.Notes;

namespace WebPortal.DbStuff.Repositories.Interfaces.Notes;

public interface ITagRepository : IBaseDbRepository<Tag>
{
    List<string> GetTagNamesByIds(List<int> tagIds);
}