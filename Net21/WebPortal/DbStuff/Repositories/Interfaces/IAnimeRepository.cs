using WebPortal.DbStuff.Models;

namespace WebPortal.DbStuff.Repositories.Interfaces;

public interface IAnimeRepository : IBaseRepository<Anime>
{
    Anime GetFirst();
}