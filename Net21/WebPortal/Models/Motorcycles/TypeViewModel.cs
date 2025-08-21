using WebPortal.DbStuff.Models.Motorcycles;

namespace WebPortal.Models.Motorcycles
{
    public class TypeViewModel
    {
        public string TypeName { get; set; }
        public string? Description { get; set; }
        public virtual List<Motorcycle> Motorcycles { get; set; } = new List<Motorcycle>();
    }
}
