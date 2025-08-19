using WebPortal.DbStuff.Models.Marketplace;

namespace WebPortal.Models.Marketplace
{
<<<<<<< Updated upstream
    public class LaptopViewModel : ProductBaseViewModel
=======
    public class LaptopViewModel : ProductViewModel
>>>>>>> Stashed changes
    {
        public string Processor { get; set; }
        public int RAM { get; set; }
        public int Storage { get; set; }
        public string OS { get; set; }
        public double ScreenSize { get; set; }
<<<<<<< Updated upstream
        //public List<Laptop> AllLaptops { get; set; }
=======
        public List<LaptopViewModel> Laptops { get; set; } = new();
>>>>>>> Stashed changes
    }
}
