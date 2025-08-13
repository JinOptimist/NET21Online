using WebPortal.DbStuff.Models;

namespace WebPortal.DbStuff.Repositories
{
    public interface IGirlRepository : IBaseRepository<Girl>
    {
        List<Girl> GetMostPopular();
    }
}