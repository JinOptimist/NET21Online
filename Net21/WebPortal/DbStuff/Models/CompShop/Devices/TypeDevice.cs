namespace WebPortal.DbStuff.Models.CompShop.Devices
{
    /// <summary>
    /// Type of device by application (gaming, office)
    /// </summary>
    public class TypeDevice : BaseModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual Device Device { get; set; }
    }
}
