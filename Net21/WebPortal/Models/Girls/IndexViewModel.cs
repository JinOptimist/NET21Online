namespace WebPortal.Models.Girls
{
    public class IndexViewModel
    {
        public List<GirlViewModel> GirlsFromDb { get; set; }

        public List<GirlFromApiViewModel> GirlFromApi { get; set; }
    }
}
