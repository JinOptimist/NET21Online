using WebPortal.DbStuff.Models;

namespace WebPortal.Models.UnderTheBridge
{
    public class AddGuitarViewModel
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public GuitarEntity.AccessStatus Status { get; set; }
    }
}
