
using IdolMinimalApi.DbStuff;
using IdolMinimalApi.DbStuff.Models;
using System.Net.WebSockets;

namespace IdolMinimalApi
{
    public class IdolService
    {
        private IdolDbContext _idolDbContext;

        public IdolService(IdolDbContext idolDbContext)
        {
            _idolDbContext = idolDbContext;
        }

        public List<string> GetNames()
        {
            return _idolDbContext.Idols.Select(x=>x.Name).ToList();
        }

        public int CreateIdol(string name, string url)
        {
            var idol = new Idol
            {
                Name = name,
                Url = url
            };
            _idolDbContext.Idols.Add(idol);
            _idolDbContext.SaveChanges();
            return idol.Id;
        }
    }
}
