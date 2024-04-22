using System;
using System.Collections.Generic;
using NUnit.Framework;
using client.models;
using client.repositories;
using Moq;
using client.services;

namespace client.tests
{
    [TestFixture]
    public class PostSavedServiceTests
    {
        private IPostSavedService _postSavedService;
        private Mock<IPostSavedRepository> _mockPostSavedRepository;

        [SetUp]
        public void Setup()
        {
            _mockPostSavedRepository = new Mock<IPostSavedRepository>();
            _postSavedService = new PostSavedService(_mockPostSavedRepository.Object);
        }

        [Test]
        public void AddPostSaved_ValidPostSaved_ReturnsTrue()
        {
            // Arrange
            var postSaved = new PostSaved(Guid.NewGuid(),
                                          Guid.Parse("8EB731DD-07DC-41DB-A576-6AB50C326EA4"),
                                          Guid.Parse("D6666B72-7E1F-4A8D-9226-38F6DA717A77"));
            _mockPostSavedRepository.Setup(repo => repo.addPostSavedtoDB(postSaved)).Returns(true);

            // Act
            bool result = _postSavedService.addPostSaved(postSaved);

            // Assert
            Assert.IsTrue(result, "Adding post saved failed.");
        }

        [Test]
        public void RemovePostSaved_ValidPostSaved_ReturnsTrue()
        {
            // Arrange
            var postSaved = new PostSaved(Guid.NewGuid(),
                                          Guid.Parse("8EB731DD-07DC-41DB-A576-6AB50C326EA4"),
                                          Guid.Parse("D6666B72-7E1F-4A8D-9226-38F6DA717A77"));
            _mockPostSavedRepository.Setup(repo => repo.removePostSavedFromDB(postSaved)).Returns(true);

            // Act
            bool result = _postSavedService.removePostSaved(postSaved);

            // Assert
            Assert.IsTrue(result, "Removing post saved failed.");
        }

        [Test]
        public void GetPostSavedList_ReturnsListOfPostSaved()
        {
            // Arrange
            var expectedList = new List<PostSaved>
            {
                new PostSaved(Guid.NewGuid(),
                              Guid.Parse("8EB731DD-07DC-41DB-A576-6AB50C326EA4"),
                              Guid.Parse("D6666B72-7E1F-4A8D-9226-38F6DA717A77")),
                new PostSaved(Guid.NewGuid(),
                              Guid.Parse("8F8A147A-5B80-47E9-A092-A675F439A515"),
                              Guid.Parse("D6666B72-7E1F-4A8D-9226-38F6DA717A77"))
            };

            _mockPostSavedRepository.Setup(repo => repo.getAll()).Returns(expectedList);

            // Act
            List<PostSaved> result = _postSavedService.getPostSavedList();

            // Assert
            Assert.AreEqual(expectedList.Count, result.Count, "Retrieved list count does not match expected count.");
            CollectionAssert.AreEqual(expectedList, result, "Retrieved list does not match expected list.");
        }
    }
}
