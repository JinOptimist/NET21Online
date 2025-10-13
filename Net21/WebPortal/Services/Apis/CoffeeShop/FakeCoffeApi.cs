namespace WebPortal.Services.Apis.CoffeeShop
{
    public class FakeCoffeApi
    {
        private readonly HttpClient _httpClient;

        public FakeCoffeApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<string>> GetRandomCoffeeImages(int count = 10, int maxConcurrency = 5)
        {
            var semaphore = new SemaphoreSlim(maxConcurrency);
            var tasks = new List<Task<string?>>();

            for (int i = 0; i < count; i++)
            {
                tasks.Add(GetSingleCoffeeImageWithSemaphore(semaphore));
            }

            var results = await Task.WhenAll(tasks);
            return results.Where(x => x != null).ToList()!;
        }

        private async Task<string?> GetSingleCoffeeImageWithSemaphore(SemaphoreSlim semaphore)
        {
            await semaphore.WaitAsync();
            try
            {
                return await GetSingleCoffeeImage();
            }
            finally
            {
                semaphore.Release();
            }
        }

        private async Task<string?> GetSingleCoffeeImage()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<CoffeeImageResponse>("random.json");
                return response?.File;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка загрузки изображения: {ex.Message}");
                return null;
            }
        }

        public class CoffeeImageResponse
        {
            public string File { get; set; }
        }
    }
}