namespace WebPortal.Models.CompShop.Devices
{
    /// <summary>
    /// Type of device by application (gaming, office)
    /// </summary>
    public class TypeDevice()
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
