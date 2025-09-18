using WebPortal.DbStuff.DataModels;
using WebPortal.Enum;

namespace WebPortal.Models.Users
{
    public class ProfileViewModel
    {
        public List<Language> Languages { get; set; }
        public Language Language { get; set; }
        public string AvatarUrl { get; set; }
        public string Name { get; set; }
        public Role Role { get; set; }

        public List<AuthorStatisticForAnimeModel> AuthorStatisticForAnime { get; set; }
    }
}
