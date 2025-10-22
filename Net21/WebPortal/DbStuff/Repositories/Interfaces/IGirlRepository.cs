using WebPortal.DbStuff.Models;
using WebPortal.Enum;

namespace WebPortal.DbStuff.Repositories.Interfaces
{
    public interface IGirlRepository : IBaseRepository<Girl>
    {
        List<Girl> GetMostPopular();
        List<Girl> GetAllWithAuthor();
        bool IsUniqName(string? name);
        List<Girl> GetAllAfterSort(string? fieldName, SortDirection sortDirection);
    }
}