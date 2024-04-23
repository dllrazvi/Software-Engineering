using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using client.models;
using client.repositories;
using client.services;
using Moq;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class LocationServiceTests
    {
        private Mock<ILocationRepository> _mockLocationRepository;
        private ILocationService _locationService;

        [SetUp]
        public void Setup()
        {
            _mockLocationRepository = new Mock<ILocationRepository>();
            _locationService = new LocationService(_mockLocationRepository.Object);
        }

        [Test]
        public async Task GetLocationById_ValidId_ReturnsLocation()
        {
            // Arrange
            string locationId = "123456";
            Location expectedLocation = new Location(locationId, "Test Location", 1.23, 4.56);
            _mockLocationRepository
                .Setup(r => r.GetLocationDetails(locationId))
                .ReturnsAsync(expectedLocation);

            // Act
            var location = await _locationService.GetLocationById(locationId);

            // Assert
            Assert.IsNotNull(location);
            Assert.AreEqual(expectedLocation, location);
        }

        [Test]
        public async Task GetLocationById_InvalidId_ReturnsNull()
        {
            // Arrange
            string locationId = "InvalidId";
            _mockLocationRepository
                .Setup(r => r.GetLocationDetails(locationId))
                .ReturnsAsync((Location)null);

            // Act
            var location = await _locationService.GetLocationById(locationId);

            // Assert
            Assert.IsNull(location);
        }

        [Test]
        public async Task SearchLocations_ValidQuery_ReturnsLocations()
        {
            // Arrange
            string query = "New York";
            List<Location> expectedLocations = new List<Location>
            {
                new Location("1", "Location 1", 1.23, 4.56),
                new Location("2", "Location 2", 2.34, 5.67)
            };
            _mockLocationRepository
                .Setup(r => r.SearchLocations(query))
                .ReturnsAsync(expectedLocations);

            // Act
            var locations = await _locationService.SearchLocations(query);

            // Assert
            Assert.IsNotNull(locations);
            Assert.AreEqual(expectedLocations, locations);
        }

        [Test]
        public async Task GetLocationById_NullId_ReturnsNull()
        {
            // Arrange
            string locationId = null;

            // Act
            var location = await _locationService.GetLocationById(locationId);

            // Assert
            Assert.IsNull(location);
        }

        [Test]
        public async Task GetLocationById_EmptyId_ReturnsNull()
        {
            // Arrange
            string locationId = "";

            // Act
            var location = await _locationService.GetLocationById(locationId);

            // Assert
            Assert.IsNull(location);
        }

        [Test]
        public async Task SearchLocations_NullQuery_ReturnsEmptyList()
        {
            // Arrange
            string query = null;

            // Act
            var locations = await _locationService.SearchLocations(query);

            // Assert
            Assert.IsNotNull(locations);
            Assert.IsEmpty(locations);
        }

        [Test]
        public async Task SearchLocations_EmptyQuery_ReturnsEmptyList()
        {
            // Arrange
            string query = "";

            // Act
            var locations = await _locationService.SearchLocations(query);

            // Assert
            Assert.IsNotNull(locations);
            Assert.IsEmpty(locations);
        }

    }
}
