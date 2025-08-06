namespace WebPortal.Models
{
    public class DeviceModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Typedevice TypeDevice { get; set; }

        public DeviceCategory DeviceCategory { get; set; }

        public double Price { get; set; }

        public string Image { get; set; }

        public bool IsPopular { get; set; }
    }
}
