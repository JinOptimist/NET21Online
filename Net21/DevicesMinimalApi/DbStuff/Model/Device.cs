namespace DevicesMinimalApi.DbStuff.Model
{
    public class Device : BaseDBModel
    {
        public string Name { get; set; }

        public string UrlImage { get; set; }

        public string CategoryName { get; set; }

        public decimal Price { get; set; }
    }
}
