using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebPortal.DbStuff.Models;
using WebPortal.DbStuff.Repositories.Interfaces;
using WebPortal.Enum;

namespace WebPortal.DbStuff.Repositories
{
    public class GirlRepository : BaseRepository<Girl>, IGirlRepository
    {
        public GirlRepository(WebPortalContext portalContext) : base(portalContext)
        {
        }

        public List<Girl> GetAllWithAuthor()
        {
            return _dbSet
                .Include(x => x.Author)
                .ToList();
        }

        public List<Girl> GetMostPopular()
        {
            return _dbSet
                .Include(x => x.Author)
                .OrderBy(x => x.Size)
                .Take(50)
                .ToList();
        }

        public List<Girl> GetAllAfterSort(string? fieldName, SortDirection sortDirection)
        {
            var query = _dbSet.Include(x => x.Author).AsQueryable();

            if (fieldName is null)
            {
                return query.ToList();
            }

            //if (fieldName == "Age")
            //{
            //    query = query.OrderBy(girl => girl.Author.Money);
            //}

            // girl
            var model = Expression.Parameter(typeof(Girl), "girl");

            // if fieldName == "Author.Money" => ["Author", "Money"]
            var fields  = fieldName.Split('.');

            // girl.Author
            var lastPropertyType = model.Type.GetProperty(fields[0])!.PropertyType;
            var property = Expression.Property(model, fields[0]);
            for (int i = 1; i < fields.Length; i++)
            {
                // girl.Author.Money
                lastPropertyType = property.Type.GetProperty(fields[i])!.PropertyType;
                property = Expression.Property(property, fields[i]);

                // girl.Author.Money < 0
                // Expression.LessThan(property, Expression.Constant(0));
            }

            // girl => girl.Age
            var lambda = Expression.Lambda(property, model);

            // Just for example
            // Func<Girl, int> method = MyLambda;

            // query = query.OrderByDescending(lambda);
            var mehtodName = sortDirection == SortDirection.Ascending
                ? "OrderBy"
                : "OrderByDescending";

            var method = typeof(Queryable)
                .GetMethods()
                .First(method => method.Name == mehtodName && method.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(Girl), lastPropertyType);
            query = (IQueryable<Girl>)method.Invoke(null, [query, lambda])!;


            //var lambdaForWhere = Expression.Lambda<Func<Girl, bool>>(property, model);
            //query.Where(lambdaForWhere);

            return query.ToList();
        }

        public int MyLambda(Girl girl)
        {
            return girl.Age;
        }

        public bool IsUniqName(string? name)
        {
            return !_dbSet.Any(x => x.Name == name);
        }
    }
}
