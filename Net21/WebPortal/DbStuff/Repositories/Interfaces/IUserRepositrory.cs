using WebPortal.DbStuff.DataModels;
using WebPortal.DbStuff.Models;

namespace WebPortal.DbStuff.Repositories.Interfaces
{
    public interface IUserRepositrory : IBaseRepository<User>
    {
        User? GetByName(string name);
        User? Login(string userName, string password);
        void Registration(string userName, string password);

        User? GetFirstByName(string userName);

        List<AuthorStatisticForAnimeModel> GetAuthorStatisticForAnime();
    }
}