using WebPortal.DbStuff.Models;

namespace WebPortal.DbStuff.Repositories.Interfaces
{
    public interface IBaseRepository<DbModel> where DbModel : BaseModel
    {
        DbModel Add(DbModel model);
        DbModel GetFirstById(int id);
        List<DbModel> GetAll();
        void Remove(DbModel model);
        void Remove(int id);
        void Update(DbModel model);
        bool Any();
        List<DbModel> AddRange(List<DbModel> models);
    }
}