namespace WebPortal.DbStuff.Models.Motorcycles
{
    public class Motorcycle : BaseModel
    {
        public string MotorcycleType { get; set; }
        public string Model {  get; set; }
        public string ImageSrc { get; set; }
        public string Description { get; set; }
        public virtual MotorcycleType? Type { get; set; }
        public virtual Brand? MotorcycleBrand { get; set; }
    }
}
