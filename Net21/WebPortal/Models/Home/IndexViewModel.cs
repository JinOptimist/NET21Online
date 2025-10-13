namespace WebPortal.Models.Home
{
    public class IndexViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<JokeViewModel> Jokes { get; set; } = new List<JokeViewModel>();
    }
}
