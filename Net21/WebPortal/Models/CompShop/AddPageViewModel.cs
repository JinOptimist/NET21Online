using Microsoft.AspNetCore.Mvc.Rendering;
using WebPortal.DbStuff.Models.CompShop;
using WebPortal.DbStuff.Models.CompShop.Devices;
using WebPortal.Models.CompShop.Device;

namespace WebPortal.Models.CompShop
{
    public class AddPageViewModel
    {
        public DeviceViewModel DeviceViewModel { get; set; }

        public ComputerViewModel? ComputerViewModel { get; set; }

       // public LaptopViewModel? LaptopViewModel { get; set; } И так далее

        public int CategoryId { get; set; }
        public List<SelectListItem> Categoryes { get; set; } = new List<SelectListItem>();

        public int TypeDeviceId { get; set; }
        public List<SelectListItem> TypeDevices { get; set; } = new List<SelectListItem>();
    }
}
