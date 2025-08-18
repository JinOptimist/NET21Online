namespace WebPortal.DbStuff.Models.CompShop
{
    /// <summary>
    /// Type of device by application (gaming, office)
    /// </summary>
    public class TypeDevice : BaseModel
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
