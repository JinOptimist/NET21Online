using Microsoft.EntityFrameworkCore;
using WebPortal.DbStuff.DataModels;
using WebPortal.DbStuff.Models;
using WebPortal.DbStuff.Repositories.Interfaces;

namespace WebPortal.DbStuff.Repositories
{
    public class UserRepositrory : BaseRepository<User>, IUserRepositrory
    {
        public UserRepositrory(WebPortalContext portalContext) : base(portalContext)
        {
        }

        public override User Add(User model)
        {
            throw new Exception("DO NOT USER Add. User Registration method");
        }

        public override List<User> AddRange(List<User> models)
        {
            throw new Exception("DO NOT USER Add. User Registration method");
        }

        public List<AuthorStatisticForAnimeModel> GetAuthorStatisticForAnime()
        {
            FormattableString fs = @$"SELECT 
	U.UserName AuthorName,  
	A.Name AnimeName, 
	COUNT (*) AnimeCount
FROM Animes A
	LEFT JOIN AnimeGirl AG ON A.Id = AG.AnimesId
	LEFT JOIN Girls G ON AG.CharactersId = G.Id
	LEFT JOIN Users U ON G.AuthorId = U.Id
GROUP BY U.UserName, A.Name";
            var response = _portalContext.Database
                .SqlQuery<AuthorStatisticForAnimeModel>(fs)
                .ToList();
            return response;
        }

        public User? GetByName(string name)
        {
            return _dbSet.Where(x => x.UserName == name).FirstOrDefault();
        }

        public User? Login(string userName, string password)
        {
            var hashPasswod = HashPassword(password);
            return _dbSet.FirstOrDefault(x => x.UserName == userName && x.Password == hashPasswod);
        }

        public void Registration(string userName, string password)
        {
            if (_dbSet.Any(x => x.UserName == userName))
            {
                throw new Exception($"{userName} already exist");
            }

            var user = new User
            {
                UserName = userName,
                Password = HashPassword(password), // broke Password
                AvatarUrl = "/avatar/default.jpg",
                Language = Enum.Language.English,
                Role = Enum.Role.User
            };

            _dbSet.Add(user);
            _portalContext.SaveChanges();
        }

        private string HashPassword(string password)
        {
            return password.Replace("a", "") + password.Length;
        }

        public User? GetFirstByName(string userName)
        {
            return _dbSet.FirstOrDefault(x => x.UserName == userName);
        }
    }
}
