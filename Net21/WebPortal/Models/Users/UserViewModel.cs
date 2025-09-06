using WebPortal.Enum;

namespace WebPortal.Models.Users
{
    public class UserViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string AvatarUrl { get; set; }
        public int Money { get; set; }
        public Role Role { get; set; }
    }
}
