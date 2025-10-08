namespace WebPortal.Services.Apis
{
    public class WeatherApi
    {
        private HttpClient _httpClient;

        public WeatherApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<WeatherResponse> GetCurrentWeather(double latitude = 20, double longitude = 30)
        {
            var url = $"/v1/forecast?latitude={latitude}&longitude={longitude}&current=temperature_2m,wind_speed_10m&hourly=temperature_2m,relative_humidity_2m,wind_speed_10m";
            var weather = await _httpClient.GetFromJsonAsync<WeatherResponse>(url);
            return weather;
        }
    }

    public class WeatherResponse
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Timezone { get; set; }
        public string Timezone_abbreviation { get; set; }
        public double Elevation { get; set; }
        public CurrentUnits Current_units { get; set; }
        public Current Current { get; set; }
        public HourlyUnits Hourly_units { get; set; }
        public Hourly Hourly { get; set; }
    }

    public class CurrentUnits
    {
        public string Time { get; set; }
        public string Interval { get; set; }
        public string Temperature_2m { get; set; }
        public string Wind_speed_10m { get; set; }
    }

    public class Current
    {
        public string Time { get; set; }
        public int Interval { get; set; }
        public double Temperature_2m { get; set; }
        public double Wind_speed_10m { get; set; }
    }

    public class HourlyUnits
    {
        public string Time { get; set; }
        public string Temperature_2m { get; set; }
        public string Relative_humidity_2m { get; set; }
        public string Wind_speed_10m { get; set; }
    }

    public class Hourly
    {
        public List<string> Time { get; set; }
        public List<double> Temperature_2m { get; set; }
        public List<int> Relative_humidity_2m { get; set; }
        public List<double> Wind_speed_10m { get; set; }
    }
}