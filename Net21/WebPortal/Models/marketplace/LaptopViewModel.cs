using WebPortal.DbStuff.Models.Marketplace;
using WebPortal.Models.marketplace.BaseViewModel;

namespace WebPortal.Models.Marketplace
{
    public class LaptopViewModel : ProductBase
    {
        public string Processor { get; set; }
        public int RAM { get; set; }
        public int Storage { get; set; }
        public string OS { get; set; }
        public double ScreenSize { get; set; }
        public List<Laptop> AllLaptops { get; set; }
    }
}
