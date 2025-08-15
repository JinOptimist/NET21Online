namespace WebPortal.DbStuff.Models
{
    public class User : BaseModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string AvatarUrl { get; set; }
        public int Money { get; set; }
    }
}
