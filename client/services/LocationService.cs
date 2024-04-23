using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using client.models;
using client.repositories;

namespace client.services
{
    internal interface ILocationService
    {
        Task<Location> GetLocationById(string locationId);
        Task<List<Location>> SearchLocations(string querry);
    }

    internal class LocationService : ILocationService
    {
        private ILocationRepository _locationRepository;

        public LocationService(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public async Task<Location> GetLocationById(String locationId)
        {
            return await _locationRepository.GetLocationDetails(locationId);
        }

        public async Task<List<Location>> SearchLocations(String query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return new List<Location>();
            }

            return await _locationRepository.SearchLocations(query);
        }
    }
}
