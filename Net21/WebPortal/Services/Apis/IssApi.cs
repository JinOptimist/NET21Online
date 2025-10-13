namespace WebPortal.Services.Apis
{
    public class IssApi
    {
        private readonly HttpClient _httpClient;

        public IssApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IssPositionResponse> GetCurrentIssPosition()
        {
            var url = "https://api.wheretheiss.at/v1/satellites/25544";
            var position = await _httpClient.GetFromJsonAsync<IssPositionResponse>(url);
            return position;
        }

        public async Task<DistanceResponse> CalculateDistanceToIss(double userLatitude, double userLongitude)
        {
            var issPosition = await GetCurrentIssPosition();

            var distance = CalculateDistance(userLatitude, userLongitude,
                                           issPosition.Latitude, issPosition.Longitude);

            return new DistanceResponse
            {
                UserPosition = new Position { Latitude = userLatitude, Longitude = userLongitude },
                IssPosition = new Position { Latitude = issPosition.Latitude, Longitude = issPosition.Longitude },
                DistanceKilometers = distance,
                Timestamp = DateTime.UtcNow
            };
        }

        private double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            const double earthRadiusKm = 6371;

            var dLat = ToRadians(lat2 - lat1);
            var dLon = ToRadians(lon2 - lon1);

            lat1 = ToRadians(lat1);
            lat2 = ToRadians(lat2);

            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2) * Math.Cos(lat1) * Math.Cos(lat2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return earthRadiusKm * c;
        }

        private double ToRadians(double degrees)
        {
            return degrees * Math.PI / 180;
        }
    }

    public class IssPositionResponse
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Altitude { get; set; }
        public double Velocity { get; set; }
        public string Visibility { get; set; }
        public double Footprint { get; set; }
        public long Timestamp { get; set; }
        public double Daynum { get; set; }
        public double Solar_Lat { get; set; }
        public double Solar_Lon { get; set; }
        public string Units { get; set; }
    }

    public class DistanceResponse
    {
        public Position UserPosition { get; set; }
        public Position IssPosition { get; set; }
        public double DistanceKilometers { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public class Position
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}