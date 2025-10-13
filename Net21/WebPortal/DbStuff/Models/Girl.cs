namespace WebPortal.DbStuff.Models
{
    public class Girl : BaseModel
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public int Age { get; set; }
        public int? Size { get; set; }

        public virtual User? Author { get; set; }
        public virtual List<User> UserWhoAddToFavorite { get; set; } = new List<User>();

        public virtual List<Anime> Animes { get; set; }

        public virtual List<Festival> Festivals { get; set; } = new List<Festival>();
    }
}
