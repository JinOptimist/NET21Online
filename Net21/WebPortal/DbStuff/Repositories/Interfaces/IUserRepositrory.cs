using WebPortal.DbStuff.Models;

namespace WebPortal.DbStuff.Repositories.Interfaces
{
    public interface IUserRepositrory : IBaseRepository<User>
    {
        User Login(string userName, string password);
        void Registration(string userName, string password);
    }
}