namespace WebPortal.DbStuff.Models.Motorcycles
{
    public class MotorcycleType : BaseModel
    {
        public string TypeName { get; set; }
        public string Description { get; set; }
        public virtual List<Motorcycle> Motorcycles { get; set; } = new List<Motorcycle>();
    }
}
