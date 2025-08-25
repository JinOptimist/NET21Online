using Microsoft.AspNetCore.Mvc.Rendering;
using WebPortal.DbStuff.Models.CompShop;
using WebPortal.DbStuff.Models.CompShop.Devices;
using WebPortal.DbStuff.Models.Marketplace;
using WebPortal.Models.CompShop.Device;

namespace WebPortal.Models.CompShop
{
    public class ProductInfoViewModel
    {
        public DeviceViewModel DeviceViewModel { get; set; }

        public int ComputerId { get; set; }
        public ComputerViewModel? ComputerViewModel { get; set; }

        /*public int LaptopId { get; set; }
        public LaptopViewModel? TypeDevice { get; set; } И так далее */

    }
}
