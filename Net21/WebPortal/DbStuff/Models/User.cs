using WebPortal.DbStuff.Models.CoffeShop;
using WebPortal.Enum;

using WebPortal.DbStuff.Models.Tourism;

namespace WebPortal.DbStuff.Models
{
    public class User : BaseModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string AvatarUrl { get; set; }
        public int Money { get; set; }
        public Role Role { get; set; }
        public Language Language { get; set; }

        public virtual List<Girl> CreatedGirls { get; set; } = new List<Girl>();
        public virtual List<Girl> FavoriteGirls { get; set; } = new List<Girl>();
        public virtual List<SpaceNews> SpaceNewsAuthorship { get; set; } = new List<SpaceNews>();
        public virtual List<CommentEntity> CommentsForGuitar { get; set; } = new List<CommentEntity>();
        public virtual List<CoffeeProduct> CreatedCoffe { get; set; } = new List<CoffeeProduct>();
        public virtual List<Product> Products { get; set; } = new List<Product>();
        public virtual List<Tours> CreatedTours { get; set; } = new List<Tours>();
    }
}
