using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using client.models;
using Newtonsoft.Json.Linq;

namespace client.repositories
{
    internal interface ILocationRepository
    {
        Task<Location> GetLocationDetails(string locationId);
        Task<List<Location>> SearchLocations(string query);
    }

    internal class LocationRepository : ILocationRepository
    {
        private readonly HttpClient _httpClient;

        public LocationRepository(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<List<Location>> SearchLocations(string query)
        {
            string requestURL = $"https://example.com/search?query={query}";

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(requestURL);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    List<Location> locations = ParseSearchLocationsJsonResponse(jsonResponse);
                    return locations;
                }
                else
                {
                    // Handle unsuccessful response
                    return new List<Location>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }

        static List<Location> ParseSearchLocationsJsonResponse(string jsonResponse)
        {
            List<Location> locations = new List<Location>();

            JObject root = JObject.Parse(jsonResponse);
            JArray results = (JArray)root["results"];

            foreach (var result in results)
            {
                Location location = new Location(
                    result["place_id"].ToString(),
                    result["name"].ToString(),
                    (double)result["geometry"]["location"]["lat"],
                    (double)result["geometry"]["location"]["lng"]
                );
                locations.Add(location);
            }

            return locations;
        }

        public async Task<Location> GetLocationDetails(string locationId)
        {
            string requestURL = $"https://example.com/details?id={locationId}";

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(requestURL);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    return ParseGetLocationDetailsJsonResponse(jsonResponse);
                }
                else
                {
                    // Handle unsuccessful response
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }

        static Location ParseGetLocationDetailsJsonResponse(string jsonResponse)
        {
            JObject root = JObject.Parse(jsonResponse);

            string locationId = root["result"]["place_id"].ToString();
            string locationName = root["result"]["name"].ToString();
            double locationLatitude = (double)root["result"]["geometry"]["location"]["lat"];
            double locationLongitude = (double)root["result"]["geometry"]["location"]["lng"];

            return new Location(locationId, locationName, locationLatitude, locationLongitude);
        }
    }
}
