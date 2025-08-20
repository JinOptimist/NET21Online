using WebPortal.DbStuff.Models;

namespace WebPortal.DbStuff.Repositories.Interfaces.Notes;

public interface IBaseDbRepository<TDbModel> where TDbModel : BaseModel
{
    TDbModel Add(TDbModel model);
    List<TDbModel> GetAll();
    void Remove(TDbModel model);
    void Remove(int id);
}