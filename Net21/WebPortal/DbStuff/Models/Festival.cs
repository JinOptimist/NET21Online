using WebPortal.Enum;

namespace WebPortal.DbStuff.Models
{
    public class Festival : BaseModel
    {
        public string Name { get; set; }
        public string Slogan { get; set; }
        public DateTime Date { get; set; }
        public FestivalTheme Theme { get; set; }
        public string LogoUrl { get; set; }
        public string? Description { get; set; }

        public virtual List<Girl> Girls { get; set; } = new List<Girl>();
    }
}
