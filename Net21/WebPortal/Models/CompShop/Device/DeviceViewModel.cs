using WebPortal.DbStuff.Models.CompShop;

namespace WebPortal.Models.CompShop.Device
{
    public class DeviceViewModel
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public int TypeDeviceId { get; set; }

        public int CategoryId { get; set; }

        public double? Price { get; set; }

        public string? Image { get; set; }

        public bool IsPopular { get; set; }

        public TypeDevice TypeDevice { get; set; }
        public Category Category { get; set; }
    }
}
