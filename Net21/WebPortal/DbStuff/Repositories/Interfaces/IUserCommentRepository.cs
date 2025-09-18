using WebPortal.DbStuff.Models.CoffeShop;

namespace WebPortal.DbStuff.Repositories.Interfaces
{
    public interface IUserCommentRepository : IBaseRepository<UserComment>
    {
        bool IsUniqNameCoffeUser(string? name);
    }
}