using WebPortal.DbStuff.Models.CoffeShop;

namespace WebPortal.DbStuff.Models
{
    public class User : BaseModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string AvatarUrl { get; set; }
        public int Money { get; set; }

        public virtual List<Girl> CreatedGirls { get; set; } = new List<Girl>();
        public virtual List<Girl> FavoriteGirls { get; set; } = new List<Girl>();
        public virtual List<SpaceNews> SpaceNewsAuthorship { get; set; } = new List<SpaceNews>();
        public virtual List<CommentEntity> CommentsForGuitar { get; set; } = new List<CommentEntity>();
        public virtual List<CoffeeProduct> CreatedCoffe { get; set; } = new List<CoffeeProduct>();
    }
}
