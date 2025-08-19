namespace WebPortal.DbStuff.Models.Motorcycles
{
    public class Brand : BaseModel
    {
        public string BrandName { get; set; }
        public virtual List<Motorcycle> Motorcycles { get; set; } = new List<Motorcycle>();
    }
}
