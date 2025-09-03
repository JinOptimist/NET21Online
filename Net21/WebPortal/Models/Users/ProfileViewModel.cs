using WebPortal.Enum;

namespace WebPortal.Models.Users
{
    public class ProfileViewModel
    {
        public List<Language> Languages { get; set; }
        public Language Language { get; set; }
        public string Name { get; set; }
    }
}
