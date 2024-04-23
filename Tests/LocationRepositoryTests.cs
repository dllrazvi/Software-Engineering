using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using client.repositories;
using Moq;
using Moq.Protected;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class LocationRepositoryTests
    {
        private Mock<HttpMessageHandler> _mockHttpMessageHandler;
        private HttpClient _httpClient;
        private ILocationRepository _locationRepository;

        [SetUp]
        public void Setup()
        {
            _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            _httpClient = new HttpClient(_mockHttpMessageHandler.Object);
            _locationRepository = new LocationRepository(_httpClient);
        }

        [TearDown]
        public void TearDown()
        {
            _httpClient.Dispose();
        }

        [Test]
        public async Task SearchLocations_ValidQuery_ReturnsLocations()
        {
            // Arrange
            string query = "New York";
            var jsonResponse = "{\"results\":[{\"place_id\":\"1\",\"name\":\"Location 1\",\"geometry\":{\"location\":{\"lat\":1.23,\"lng\":4.56}}}]}";
            SetupHttpMessageHandlerResponse(HttpStatusCode.OK, jsonResponse);

            // Act
            var locations = await _locationRepository.SearchLocations(query);

            // Assert
            Assert.IsNotNull(locations);
            Assert.IsNotEmpty(locations);
            Assert.AreEqual("1", locations[0].Id);
            Assert.AreEqual("Location 1", locations[0].Name);
            Assert.AreEqual(1.23, locations[0].Latitude);
            Assert.AreEqual(4.56, locations[0].Longitude);
        }

        [Test]
        public async Task SearchLocations_HandlesHttpError_ReturnsEmptyList()
        {
            // Arrange
            string query = "New York";
            SetupHttpMessageHandlerResponse(HttpStatusCode.InternalServerError); // Simulate internal server error

            // Act
            var locations = await _locationRepository.SearchLocations(query);

            // Assert
            Assert.IsNotNull(locations);
            Assert.IsEmpty(locations);
        }

        [Test]
        public async Task GetLocationDetails_HandlesException_ReturnsNull()
        {
            // Arrange
            string locationId = "123456";
            SetupHttpMessageHandlerResponse(HttpStatusCode.OK, "Invalid JSON Response"); // Simulate invalid JSON response
            _mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ThrowsAsync(new HttpRequestException("Simulated exception")); // Simulate HTTP request exception

            // Act
            var location = await _locationRepository.GetLocationDetails(locationId);

            // Assert
            Assert.IsNull(location);
        }


        private void SetupHttpMessageHandlerResponse(HttpStatusCode statusCode, string content = "")
        {
            _mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = statusCode,
                    Content = new StringContent(content)
                });
        }
    }
}
