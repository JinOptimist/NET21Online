using WebPortal.DbStuff.Models;

namespace WebPortal.DbStuff.Repositories.Interfaces
{
    public interface IGirlRepository : IBaseRepository<Girl>
    {
        List<Girl> GetMostPopular();
        List<Girl> GetAllWithAuthor();
    }
}