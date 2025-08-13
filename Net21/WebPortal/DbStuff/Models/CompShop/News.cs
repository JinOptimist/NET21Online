namespace WebPortal.DbStuff.Models.CompShop
{
    public class News
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Text { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public DateTime DateCreate { get; private set; } = DateTime.Now;
    }
}
