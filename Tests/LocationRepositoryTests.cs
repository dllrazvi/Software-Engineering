using NUnit.Framework;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using client.repositories;

namespace Tests
{
    [TestFixture]
    public class LocationRepositoryTests
    {
        private ILocationRepository _locationRepository;

        [SetUp]
        public void Setup()
        {
            _locationRepository = new LocationRepository();
        }

        [Test]
        public async Task SearchLocations_ValidQuery_ReturnsLocations()
        {
            // Arrange
            string query = "New York";

            // Act
            var locations = await _locationRepository.SearchLocations(query);

            // Assert
            Assert.IsNotNull(locations);
            Assert.IsNotEmpty(locations);
            foreach (var location in locations)
            {
                Assert.IsNotNull(location.Id);
                Assert.IsNotNull(location.Name);
                Assert.GreaterOrEqual(location.Latitude, -90);
                Assert.LessOrEqual(location.Latitude, 90);
                Assert.GreaterOrEqual(location.Longitude, -180);
                Assert.LessOrEqual(location.Longitude, 180);
            }
        }

        [Test]
        public async Task GetLocationDetails_ValidId_ReturnsLocation()
        {
            // Arrange
            string locationId = "123456";

            // Act
            var location = await _locationRepository.GetLocationDetails(locationId);

            // Assert
            Assert.IsNotNull(location);
            Assert.AreEqual(locationId, location.Id);
            Assert.IsNotNull(location.Name);
            Assert.GreaterOrEqual(location.Latitude, -90);
            Assert.LessOrEqual(location.Latitude, 90);
            Assert.GreaterOrEqual(location.Longitude, -180);
            Assert.LessOrEqual(location.Longitude, 180);
        }

        [Test]
        public async Task SearchLocations_InvalidQuery_ReturnsEmptyList()
        {
            // Arrange
            string query = "InvalidQuery";

            // Act
            var locations = await _locationRepository.SearchLocations(query);

            // Assert
            Assert.IsNotNull(locations);
            Assert.IsEmpty(locations);
        }

        [Test]
        public async Task GetLocationDetails_InvalidId_ReturnsNull()
        {
            // Arrange
            string locationId = "InvalidId";

            // Act
            var location = await _locationRepository.GetLocationDetails(locationId);

            // Assert
            Assert.IsNull(location);
        }
    }
}
