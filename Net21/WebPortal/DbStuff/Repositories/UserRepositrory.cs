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
                AvatarUrl = "/avatar/default.jpg"
            };

            _dbSet.Add(user);
            _portalContext.SaveChanges();
        }

        private string HashPassword(string password)
        {
            return password.Replace("a", "") + password.Length;
        }
    }
}
