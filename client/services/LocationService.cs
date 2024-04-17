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
    internal class LocationService
    {
        private LocationRepository _locationRepository;

        public LocationService(LocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public async Task<Location> GetLocationById(String locationId)
        {
            return await _locationRepository.GetLocationDetails(locationId);
        }

		public async Task<List<Location>> SearchLocations(String querry)
		{
			return await _locationRepository.SearchLocations(querry);
		}
	}
}
