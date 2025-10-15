using System.Text.Json;

namespace WebPortal.Services.Apis.MarketplaceApis
{
    public class ExchangeRatesApi
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public ExchangeRatesApi(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<ExchangeRatesResponse> GetCurrentRatesAsync()
        {
            try
            {
                var baseUrl = "https://api.exchangerate-api.com/v4/latest/USD";

                var response = await _httpClient.GetStringAsync(baseUrl);
                var exchangeRates = JsonSerializer.Deserialize<ExchangeRatesResponse>(response, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return exchangeRates;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ExchangeRatesResponse> GetRatesByDateAsync(DateTime date)
        {
            try
            {
                var dateString = date.ToString("yyyy-MM-dd");
                var baseUrl = $"https://api.exchangerate-api.com/v4/history/USD/{dateString}";

                var response = await _httpClient.GetStringAsync(baseUrl);
                var exchangeRates = JsonSerializer.Deserialize<ExchangeRatesResponse>(response, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return exchangeRates;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ExchangeRatesResponse>> GetRatesForPeriodAsync(DateTime startDate, DateTime endDate)
        {
            var rates = new List<ExchangeRatesResponse>();
            var currentDate = startDate;

            while (currentDate <= endDate)
            {
                var rate = await GetRatesByDateAsync(currentDate);
                if (rate != null)
                {
                    rates.Add(rate);
                }
                currentDate = currentDate.AddDays(1);
            }

            return rates;
        }
    }

    public class ExchangeRatesResponse
    {
        public string Base { get; set; }
        public string Date { get; set; }
        public Dictionary<string, decimal> Rates { get; set; }
    }

    public class ExchangeRateViewModel
    {
        public string Currency { get; set; }
        public decimal Rate { get; set; }
        public decimal Change { get; set; }
        public string ChangePercent { get; set; }
    }
}
