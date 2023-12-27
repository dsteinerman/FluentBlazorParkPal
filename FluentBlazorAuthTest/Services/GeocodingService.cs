using System.Text.Json;

namespace FluentBlazorAuthTest.Services
{
    public class GeocodingService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "AIzaSyC9xeR-FS8PtMpGf9sTiy3NrysMX9rcoSI"; // This API Key is secured and is for testing only.

        public GeocodingService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<(double Latitude, double Longitude, string Status)> GeocodeAddress(string address)
        {
            var url = $"https://maps.googleapis.com/maps/api/geocode/json?address={Uri.EscapeDataString(address)}&key={_apiKey}";
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
                var root = json.RootElement;
                var status = root.GetProperty("status").GetString();

                if (status == "OK")
                {
                    var location = root.GetProperty("results")[0].GetProperty("geometry").GetProperty("location");
                    var latitude = location.GetProperty("lat").GetDouble();
                    var longitude = location.GetProperty("lng").GetDouble();
                    return (latitude, longitude, status);
                }

                return (0, 0, status);
            }

            return (0, 0, "ERROR");
        }

        public async Task<string> ReverseGeocodeCoordinates(decimal? latitude, decimal? longitude)
        {
            var url = $"https://maps.googleapis.com/maps/api/geocode/json?latlng={latitude},{longitude}&key={_apiKey}";
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
                var root = json.RootElement;
                var status = root.GetProperty("status").GetString();

                if (status == "OK")
                {
                    var address = root.GetProperty("results")[0].GetProperty("formatted_address").GetString();
                    return address;
                }

                return $"Reverse geocoding failed: {status}";
            }

            return "Error making API call";
        }
    }
}
