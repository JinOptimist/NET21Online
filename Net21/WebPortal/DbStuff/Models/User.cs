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
    }
}
