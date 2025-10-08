namespace WebPortal.Services.Apis
{
    public class JokeApi
    {
        private HttpClient _httpClient;

        public JokeApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<JokeDto> GetRandomJoke()
        {
            var joke = await _httpClient.GetFromJsonAsync<JokeDto>("/jokes/random/");
            return joke;
        }

        public async Task<List<JokeDto>> GetTwoRandomJokes()
        {
            var tasks = new List<Task<JokeDto>>();
            
            // Создаем два параллельных запроса
            for (int i = 0; i < 2; i++)
            {
                tasks.Add(GetRandomJoke());
            }

            var jokes = await Task.WhenAll(tasks);
            return jokes.ToList();
        }
    }

    public class JokeDto
    {
        public string type { get; set; }
        public string setup { get; set; }
        public string punchline { get; set; }
        public int id { get; set; }
    }
}
