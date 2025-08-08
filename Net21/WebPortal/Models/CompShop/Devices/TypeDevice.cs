namespace WebPortal.Models.CompShop.Devices
{
    /// <summary>
    /// Device type (computer, laptop, phone...)
    /// </summary>
    public class TypeDevice()
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
