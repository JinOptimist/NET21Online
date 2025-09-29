using SpaceNewsMinApi.DbStuff;
using SpaceNewsMinApi.DbStuff.Models;

namespace SpaceNewsMinApi
{
    public class SpaceNewsService
    {
        private SpaceNewsDbContext _spaceNewsDbContext;
        public SpaceNewsService(SpaceNewsDbContext spaceNewsDbContext)
        {
            _spaceNewsDbContext = spaceNewsDbContext;
        }

        public int AddSpaceNews(string title, string content, string imageUrl)
        {
            var spaceNews = new SpaceNews
            {
                Title = title,
                Content = content,
                ImageUrl = imageUrl
            };
            _spaceNewsDbContext.SpaceNews.Add(spaceNews);
            _spaceNewsDbContext.SaveChanges();
            return spaceNews.Id;
        }
    }
}
