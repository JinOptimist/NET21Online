using static WebPortal.DbStuff.Models.GuitarEntity;

namespace WebPortal.Models.UnderTheBridge
{
    public class GuitarCreateViewModel
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public AccessStatus Status { get; set; }
    }
}
