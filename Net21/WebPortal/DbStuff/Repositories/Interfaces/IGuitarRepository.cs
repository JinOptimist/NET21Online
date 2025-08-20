using WebPortal.DbStuff.Models;

namespace WebPortal.DbStuff.Repositories.Interfaces
{
    public interface IGuitarRepository : IBaseRepository<GuitarEntity>
    {
        GuitarEntity GetById(int id);
        GuitarEntity GetByIdWithComments(int id);
        List<GuitarEntity> GetAllWithComments();
    }
}