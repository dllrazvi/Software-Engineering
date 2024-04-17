using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Threading.Tasks;

using Newtonsoft.Json;
using client.models;
using System.Security.Policy;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Windows.Input;
using System.Diagnostics.Eventing.Reader;
using client.modules;

namespace client.repositories
{
    internal class LocationRepository
    {
        private String _googlePlacesAPIKey;
        private String _searchLocationsURL;
        private String _getPlaceDetailsURL;
        private readonly HttpClient _httpClient;
        private readonly ConfigurationModule _configuration;

        public LocationRepository()
        {
            _configuration = new ConfigurationModule();

            _googlePlacesAPIKey = _configuration.getGOOGLE_PLACES_API_KEY();
            _searchLocationsURL =
                "https://maps.googleapis.com/maps/api/place/textsearch/json?key="
                + _googlePlacesAPIKey
                + "&query=";
            _getPlaceDetailsURL =
                "https://maps.googleapis.com/maps/api/place/details/json?key="
                + _googlePlacesAPIKey
                + "&fields=name" + "%2C" + "geometry" + "%2C" + "place_id" // add "%2C" between fields
                + "&place_id="
                ;
            _httpClient = new HttpClient();
        }

        public async Task<List<Location>> SearchLocations(String query)
        {
            String requestURL = _searchLocationsURL + query;

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(requestURL);

                if (response.IsSuccessStatusCode)
                {
                    String jsonResponse = await response.Content.ReadAsStringAsync();

                    List<Location> locations = ParseSearchLocationsJsonResponse(jsonResponse);
                    /*await Task.Delay(1000);*/
                    return locations;
                }
                else
                {
                    // maybe throw an exception here
                    return new List<Location>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }
        static List<Location> ParseSearchLocationsJsonResponse(String jsonResponse)
        {
            List<Location> locations = new List<Location>();

            JObject root = JObject.Parse(jsonResponse);
            JArray results = (JArray)root["results"];

            foreach (var result in results)
            {
                Location location = new Location(
                    result["place_id"].ToString(),
                    result["name"].ToString(),
                    (Double)result["geometry"]["location"]["lat"],
                    (Double)result["geometry"]["location"]["lng"]
                );
                locations.Add(location);
            }

            return locations;
        }

        public async Task<Location> GetLocationDetails(String locationId)
        {
            String requestURL = _getPlaceDetailsURL + locationId;

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(requestURL);

                if (response.IsSuccessStatusCode)
                {
                    String jsonResponse = await response.Content.ReadAsStringAsync();

                    /*await Task.Delay(1000);*/
                    return ParseGetLocationDetailsJsonResponse(jsonResponse);
                }
                else
                {
                    // maybe throw an exception here
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }

        static Location ParseGetLocationDetailsJsonResponse(String jsonResponse)
        {

            JObject root = JObject.Parse(jsonResponse);

            String locationId = root["result"]["place_id"].ToString();
            String locationName = root["result"]["name"].ToString();
            double locationLatitude = (Double)root["result"]["geometry"]["location"]["lat"];
            double locationLongitude = (Double)root["result"]["geometry"]["location"]["lng"];

            return new Location(locationId, locationName, locationLatitude, locationLongitude);
        }
    }
}
