namespace WebPortal.Models.Motorcycles
{
    public class HomeMotorcycleViewModels
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public List<MotorcyclesViewModel>? Motorcycles { get; set; }
    }
}
