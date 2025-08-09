namespace WebPortal.Models.CompShop.Devices
{
    public class ComputerViewModel : DeviceViewModel
    {
        public string Processor { get; set; }

        public int Ram { get; set; }

        public int Storage { get; set; }

        public string GraphicsCard { get; set; }

        public string Motherboard { get; set; }

        public string PowerSupply { get; set; }
    }
}
