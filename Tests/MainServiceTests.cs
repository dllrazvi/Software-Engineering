using client.models;
using client.repositories;
using client.services;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace client.tests
{
    [TestFixture]
    internal class MainServiceTests
    {
        private MainService _mainService;
        private Mock<ILocationService> _mockLocationService;
        private Mock<IPostArchivedService> _mockPostArchivedService;
        private Mock<IPostReportedService> _mockPostReportedService;
        private Mock<IPostSavedService> _mockPostSavedService;
        private Mock<IPostsService> _mockPostsService;
        private Mock<IUserService> _mockUserService;

        [SetUp]
        public void Setup()
        {
            _mockLocationService = new Mock<ILocationService>();
            _mockPostArchivedService = new Mock<IPostArchivedService>();
            _mockPostReportedService = new Mock<IPostReportedService>();
            _mockPostSavedService = new Mock<IPostSavedService>();
            _mockPostsService = new Mock<IPostsService>();
            _mockUserService = new Mock<IUserService>();

            _mainService = new MainService(
                _mockLocationService.Object,
                _mockPostArchivedService.Object,
                _mockPostReportedService.Object,
                _mockPostSavedService.Object,
                _mockPostsService.Object,
                _mockUserService.Object
            );

            // Set up mock PostsService to return a list of dummy posts
            var dummyPosts = new List<Post>
    {
        new Post { /* Initialize with sample data */ },
        new Post { /* Initialize with sample data */ },
        // Add more dummy posts if needed
    };
            _mockPostsService.Setup(service => service.getAllPosts()).Returns(dummyPosts);
        }


        [Test]
        public void GetAllPosts_CallsPostsService_ReturnsPosts()
        {
            // Arrange
            var expectedPosts = new List<Post> { /* Add sample posts here if needed */ };
            _mockPostsService.Setup(service => service.getAllPosts()).Returns(expectedPosts);

            // Act
            var result = _mainService.getAllPosts();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedPosts.Count, result.Count);
            // Add additional assertions for comparing individual posts if needed
        }

        // Add more tests for other methods in MainService as needed
    }
}
