namespace WebPortal.Services.Apis.CoffeeShop
{
    public class FakeCoffeApi
    {
        private readonly HttpClient _httpClient;

        public FakeCoffeApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<string>> GetRandomCoffeeImages(int count = 10)
        {
            var result = new List<string>();

            for (int i = 0; i < count; i++)
            {
                try
                {
                    var response = await _httpClient.GetFromJsonAsync<CoffeeImageResponse>("random.json");
                    if (response?.File != null)
                        result.Add(response.File);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка загрузки изображения: {ex.Message}");
                }
            }

            return result;
        }
    }

    public class CoffeeImageResponse
    {
        public string File { get; set; }
    }
}
