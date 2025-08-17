using WebPortal.DbStuff.Models;

namespace WebPortal.DbStuff.Repositories.Interfaces
{
    public interface IBaseRepository<DbModel> where DbModel : BaseModel
    {
        DbModel Add(DbModel model);
        List<DbModel> GetAll();
        void Remove(DbModel model);
        void Remove(int id);
    }
}