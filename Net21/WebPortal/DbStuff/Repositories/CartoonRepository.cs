using Microsoft.EntityFrameworkCore;
using WebPortal.DbStuff;
using WebPortal.Models.Cartoon;

namespace WebPortal.DbStuff.Repositories
{
    public class CartoonRepository
    {
        private readonly DbCartoonContext _context;

        public CartoonRepository(DbCartoonContext context)
        {
            _context = context;
        }

        public List<CartoonViewModel> GetAllActive()
        {
            return _context.Cartoons
                .Where(x => x.IsActive)
                .AsNoTracking()
                .ToList();
        }

        public CartoonViewModel? GetById(int id)
        {
            return _context.Cartoons
                .FirstOrDefault(x => x.Id == id && x.IsActive);
        }

        public void Save(CartoonViewModel cartoon)
        {
            if (cartoon.Id > 0)
            {
                _context.Cartoons.Update(cartoon);
            }
            else
            {
                _context.Cartoons.Add(cartoon);
            }
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var cartoon = GetById(id);
            if (cartoon != null)
            {
                cartoon.IsActive = false;
                _context.SaveChanges();
            }
        }
    }
}