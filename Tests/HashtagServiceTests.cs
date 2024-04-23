using NUnit.Framework;
using client.models;
using client.repositories;
using Moq;
using System;
using System.Collections.Generic;
using client.services;

namespace Tests
{
    [TestFixture]
    public class HashtagServiceTests
    {
        private IHashtagService _hashtagService;
        private Mock<IHashtagRepository> _hashtagRepositoryMock;

        [SetUp]
        public void Setup()
        {
            _hashtagRepositoryMock = new Mock<IHashtagRepository>();
            _hashtagService = new HashtagService(_hashtagRepositoryMock.Object);
        }

        [Test]
        public void AddHashtag_ValidName_ReturnsTrue()
        {
            // Arrange
            string hashtagName = "test";

            _hashtagRepositoryMock.Setup(repo => repo.searchHashtag(hashtagName)).Returns((Hashtag)null);
            _hashtagRepositoryMock.Setup(repo => repo.addHashtag(It.IsAny<Hashtag>())).Returns(true);

            // Act
            bool result = _hashtagService.addHashtag(hashtagName);

            // Assert
            Assert.IsTrue(result, "Adding hashtag failed.");
        }

        [Test]
        public void AddHashtag_DuplicateName_ReturnsFalse()
        {
            // Arrange
            string hashtagName = "test";

            _hashtagRepositoryMock.Setup(repo => repo.searchHashtag(hashtagName)).Returns(new Hashtag(Guid.NewGuid(), hashtagName));

            // Act
            bool result = _hashtagService.addHashtag(hashtagName);

            // Assert
            Assert.IsFalse(result, "Adding duplicate hashtag should return false.");
        }

        [Test]
        public void AddPostToHashtag_ValidData_ReturnsTrue()
        {
            // Arrange
            Guid postId = Guid.NewGuid();
            string hashtagName = "test";
            var hashtag = new Hashtag(hashtagName);

            _hashtagRepositoryMock.Setup(repo => repo.searchHashtag(hashtagName)).Returns(hashtag);
            _hashtagRepositoryMock.Setup(repo => repo.searchPostHashtagPair(postId.ToString().ToUpper(), hashtag.Id.ToString().ToUpper())).Returns(false);
            _hashtagRepositoryMock.Setup(repo => repo.addHashtagPost(postId.ToString().ToUpper(), hashtag.Id.ToString().ToUpper())).Returns(true);

            // Act
            bool result = _hashtagService.addPostToHashtag(postId, hashtagName);

            // Assert
            Assert.IsTrue(result, "Adding post to hashtag failed.");
        }

        [Test]
        public void RemovePostFromHashtag_PairNotExists_ReturnsFalse()
        {
            // Arrange
            Guid postId = Guid.NewGuid();
            string hashtagName = "test";
            var hashtag = new Hashtag(hashtagName);

            _hashtagRepositoryMock.Setup(repo => repo.searchHashtag(hashtagName)).Returns(hashtag);
            _hashtagRepositoryMock.Setup(repo => repo.searchPostHashtagPair(postId.ToString().ToUpper(), hashtag.Id.ToString().ToUpper())).Returns(false);

            // Act
            bool result = _hashtagService.removePostFromHashtag(postId, hashtagName);

            // Assert
            Assert.IsFalse(result, "Removing post from hashtag should return false if the pair does not exist.");
        }
        [Test]
        public void AddPostToHashtag_PairDoesNotExist_ReturnsTrue()
        {
            // Arrange
            Guid postId = Guid.NewGuid();
            string hashtagName = "test";
            var hashtag = new Hashtag(hashtagName);

            _hashtagRepositoryMock.Setup(repo => repo.searchHashtag(hashtagName)).Returns(hashtag);
            _hashtagRepositoryMock.Setup(repo => repo.searchPostHashtagPair(postId.ToString().ToUpper(), hashtag.Id.ToString().ToUpper())).Returns(false);
            _hashtagRepositoryMock.Setup(repo => repo.addHashtagPost(postId.ToString().ToUpper(), hashtag.Id.ToString().ToUpper())).Returns(true);

            // Act
            bool result = _hashtagService.addPostToHashtag(postId, hashtagName);

            // Assert
            Assert.IsTrue(result, "Adding post to hashtag should return true when the pair does not exist.");
        }


    }
}
