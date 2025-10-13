namespace WebPortal.Services.Apis
{
    public class CatsApi
    {
        private readonly HttpClient _httpClient;

        public CatsApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CatDto> GetRandomCat()
        {
            var cat = await _httpClient.GetFromJsonAsync<CatDto>("/cat?json=true");
            return cat;
        }

        public async Task<List<CatDto>> GetRandomCats(int catCount)
        {
            var tasks = new List<Task<CatDto>>();

            for (int i = 0; i < catCount; i++)
            {
                tasks.Add(GetRandomCat());
            }

            var cats = await Task.WhenAll(tasks);
            return cats.ToList();
        }
    }

    public class CatDto
    {
        public string Url { get; set; }
    }
}
