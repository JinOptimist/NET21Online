using WebPortal.DbStuff.Models.CompShop.Devices;

namespace WebPortal.DbStuff.Models.CompShop
{
    /// <summary>
    /// Category device (computer, laptop, phone...)
    /// </summary>
    public class Category : BaseModel
    {
        public string Name { get; set; }

        public virtual Device Device { get; set; }
    }
}
