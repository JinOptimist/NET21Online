namespace WebPortal.Services.Apis
{
    public class WaifuApi
    {
        private HttpClient _httpClient;

        public WaifuApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<string>> GetTags()
        {
            var tagsReposponse = await _httpClient.GetFromJsonAsync<TagsReposponse>("/tags");
            return tagsReposponse.versatile;
        }

        public async Task<string> GetWaifu(string tag)
        {
            var url = $"/search?included_tags={tag}";
            var waifu = await _httpClient.GetFromJsonAsync<WaifuDto>(url);
            return waifu.images.First().url;
        }
    }

    public class TagsReposponse
    {
        public List<string> versatile { get; set; }
    }

    public class WaifuDto
    {
        public List<ImagesDto> images { get; set; }
    }

    public class ImagesDto
    {
        public string url { get; set; }
    }
}
