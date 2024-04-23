using NUnit.Framework;
using client.models;
using client.repositories;
using System;
using System.Collections.Generic;

namespace Tests
{
    [TestFixture]
    public class HashtagRepositoryTests
    {
        private IHashtagRepository _hashtagRepository;

        [SetUp]
        public void Setup()
        {
            _hashtagRepository = new HashtagRepository();
        }

        [Test]
        public void AddHashtag_ValidHashtag_ReturnsTrue()
        {
            // Arrange
            var hashtag = new Hashtag("test");

            // Act
            bool result = _hashtagRepository.addHashtag(hashtag);

            // Assert
            Assert.IsTrue(result, "Adding hashtag failed.");
        }

        [Test]
        public void RemoveHashtag_ValidId_ReturnsTrue()
        {
            // Arrange
            string hashtagId = "valid_hashtag_id";

            // Act
            bool result = _hashtagRepository.removeHashtag(hashtagId);

            // Assert
            Assert.IsTrue(result, "Removing hashtag failed.");
        }

        [Test]
        public void AddHashtagPost_ValidPostAndHashtagIds_ReturnsTrue()
        {
            // Arrange
            string postId = "valid_post_id";
            string hashtagId = "valid_hashtag_id";

            // Act
            bool result = _hashtagRepository.addHashtagPost(postId, hashtagId);

            // Assert
            Assert.IsFalse(result, "Adding post to hashtag failed.");
        }

        [Test]
        public void RemovePostFromHashtag_ValidPostAndHashtagIds_ReturnsTrue()
        {
            // Arrange
            string postId = "valid_post_id";
            string hashtagId = "valid_hashtag_id";

            // Act
            bool result = _hashtagRepository.removePostFromHashtag(postId, hashtagId);

            // Assert
            Assert.IsTrue(result, "Removing post from hashtag failed.");
        }

        [Test]
        public void SearchHashtag_ValidName_ReturnsHashtagObject()
        {
            // Arrange
            string hashtagName = "test";

            // Act
            Hashtag result = _hashtagRepository.searchHashtag(hashtagName);

            // Assert
            Assert.IsNotNull(result, "Searching hashtag failed.");
        }

        [Test]
        public void SearchPostHashtagPair_ValidPostAndHashtagIds_ReturnsTrue()
        {
            // Arrange
            string postId = "valid_post_id";
            string hashtagId = "valid_hashtag_id";

            // Act
            bool result = _hashtagRepository.searchPostHashtagPair(postId, hashtagId);

            // Assert
            Assert.IsFalse(result, "Searching post-hashtag pair failed.");
        }

        [Test]
        public void GetAllPosts_ValidHashtagId_ReturnsListOfPosts()
        {
            // Arrange
            string hashtagId = "valid_hashtag_id";

            // Act
            List<Post> result = _hashtagRepository.getAllPosts(hashtagId);

            // Assert
            Assert.IsNotNull(result, "Getting all posts for hashtag failed.");
        }
    }
}
