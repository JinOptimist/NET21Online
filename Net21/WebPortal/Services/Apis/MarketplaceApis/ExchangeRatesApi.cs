using System.Text.Json;

namespace WebPortal.Services.Apis.MarketApis
{
    public class ExchangeRatesApi
    {
        private HttpClient _httpClient;
        private IConfiguration _configuration;

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
                return GetTestExchangeRates();
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
                return GetTestExchangeRates();
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

        private ExchangeRatesResponse GetTestExchangeRates()
        {
            return new ExchangeRatesResponse
            {
                Base = "USD",
                Date = DateTime.Now.ToString("yyyy-MM-dd"),
                Rates = new Dictionary<string, decimal>
                {
                    { "USD", 1.0m },
                    { "EUR", 0.85m },
                    { "GBP", 0.73m },
                    { "JPY", 110.0m },
                    { "CAD", 1.25m },
                    { "AUD", 1.35m },
                    { "CHF", 0.92m },
                    { "CNY", 6.45m },
                    { "RUB", 75.0m },
                    { "UAH", 27.0m }
                }
            };
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
