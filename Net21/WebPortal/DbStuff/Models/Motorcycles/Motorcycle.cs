namespace WebPortal.DbStuff.Models.Motorcycles
{
    public class Motorcycle : BaseModel
    {
        public string BrandName { get; set; }
        public string MotorcycleType { get; set; }
        public string Model {  get; set; }
        public string ImageSrc { get; set; }
        public string Description { get; set; }
    }
}
