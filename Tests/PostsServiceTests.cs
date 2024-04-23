using client.models;
using client.repositories;
using client.services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client.tests
{

    [TestFixture]
    internal class PostsServiceTests
    {
        private IPostsService _postsService;
        private Mock<IPostsRepository> _mockPostsRepository;

        [SetUp]
        public void Setup()
        {
            _mockPostsRepository = new Mock<IPostsRepository>();
            _postsService = new PostsService(_mockPostsRepository.Object);
        }

        [Test]
        public void AddPost_ValidPostSaved_ReturnsTrue()
        {
            var post = new Post(
                Guid.NewGuid(),
                "Test Description",
                Guid.Parse("D6666B72-7E1F-4A8D-9226-38F6DA717A77"),
                new List<Guid>(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                new Media("C:\\Users\\Raul\\Desktop\\florin.jpg"),
                0,
                "ChIJ-YqOoRUMSUcRoBLP7U19Ncw",
                DateTime.Now
            );

            this._mockPostsRepository.Setup(repository => repository.addPostToDB(It.IsAny<Post>())).Returns(true);

            var result = this._postsService.addPost(
                Guid.NewGuid(),
                "Test Description",
                new List<Guid>(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                "C:\\Users\\Raul\\Desktop\\florin.jpg",
                1,
                "ChIJ-YqOoRUMSUcRoBLP7U19Ncw"
            );

            Assert.True(result);
        }

        [Test]
        public void AddPost_InvalidMediaPath_ReturnsFalse()
        {
            // Arrange
            var invalidMediaPath = "invalid_path.jpg";

            // Act
            var result = this._postsService.addPost(
                Guid.NewGuid(),
                "Test Description",
                new List<Guid>(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                invalidMediaPath,
                1,
                "ChIJ-YqOoRUMSUcRoBLP7U19Ncw"
            );

            // Assert
            Assert.False(result);
        }

        [Test]
        public void RemovePost_PostExists_ReturnsTrue()
        {
            // Arrange
            var postId = Guid.NewGuid();

            _mockPostsRepository.Setup(repo => repo.removePostFromDB(postId)).Returns(true);

            // Act
            var result = _postsService.removePost(postId);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void UpdateDescription_ValidUpdate_ReturnsTrue()
        {
            // Arrange
            var postId = Guid.NewGuid();
            var newDescription = "Updated Description";

            _mockPostsRepository.Setup(repo => repo.updatePostDescription(postId, newDescription)).Returns(true);

            // Act
            var result = _postsService.updateDescription(postId, newDescription);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void UpdateLocation_ValidUpdate_ReturnsTrue()
        {
            // Arrange
            var postId = Guid.NewGuid();
            var newLocationID = "New Location ID";

            _mockPostsRepository.Setup(repo => repo.updatePostLocation(postId, newLocationID)).Returns(true);

            // Act
            var result = _postsService.updateLocation(postId, newLocationID);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void AddReactionToPost_ValidReaction_ReturnsTrue()
        {
            // Arrange
            var postId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var reactionType = 1;

            _mockPostsRepository.Setup(repo => repo.addReactionToPost(postId, userId, reactionType)).Returns(true);

            // Act
            var result = _postsService.addReactionToPost(postId, userId, reactionType);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void AddMentionToPost_ValidMention_ReturnsTrue()
        {
            // Arrange
            var postId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            _mockPostsRepository.Setup(repo => repo.addMentionToPost(postId, userId)).Returns(true);

            // Act
            var result = _postsService.addMentionToPost(postId, userId);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void RemoveReactionToPost_ValidRemoval_ReturnsTrue()
        {
            // Arrange
            var postId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            _mockPostsRepository.Setup(repo => repo.removeReactionToPost(postId, userId)).Returns(true);

            // Act
            var result = _postsService.removeReactionToPost(postId, userId);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void GetAllPosts_ReturnsListOfPosts()
        {
            // Arrange
            var expectedPosts = new List<Post> { /* Add sample posts here if needed */ };

            _mockPostsRepository.Setup(repo => repo.getAllPosts()).Returns(expectedPosts);

            // Act
            var posts = _postsService.getAllPosts();

            // Assert
            Assert.IsNotNull(posts);
            Assert.AreEqual(expectedPosts.Count, posts.Count);
            // Add additional assertions for comparing individual posts if needed
        }

        [Test]
        public void GetPostById_ValidPostId_ReturnsPost()
        {
            // Arrange
            var postId = Guid.NewGuid();
            var expectedPost = new Post(
                Guid.NewGuid(),
                "Test Description",
                Guid.Parse("D6666B72-7E1F-4A8D-9226-38F6DA717A77"),
                new List<Guid>(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                new Media("C:\\Users\\Raul\\Desktop\\florin.jpg"),
                0,
                "ChIJ-YqOoRUMSUcRoBLP7U19Ncw",
                DateTime.Now
            );

            _mockPostsRepository.Setup(repo => repo.getPostById(postId)).Returns(expectedPost);

            // Act
            var post = _postsService.getPostById(postId);

            // Assert
            Assert.IsNotNull(post);
            Assert.AreEqual(expectedPost, post);
            // Add additional assertions for comparing individual properties of the post if needed
        }

        [Test]
        public void GetAllPostsFromLocation_ValidLocation_ReturnsListOfPosts()
        {
            // Arrange
            var locationId = "Valid Location ID";
            var expectedPosts = new List<Post> { /* Add sample posts here if needed */ };

            _mockPostsRepository.Setup(repo => repo.getAllPostsFromLocation(locationId)).Returns(expectedPosts);

            // Act
            var posts = _postsService.getAllPostsFromLocation(locationId);

            // Assert
            Assert.IsNotNull(posts);
            Assert.AreEqual(expectedPosts.Count, posts.Count);
            // Add additional assertions for comparing individual posts if needed
        }

        [Test]
        public void GetPostLikeCount_ValidPostId_ReturnsCount()
        {
            // Arrange
            var postId = Guid.NewGuid();
            var expectedCount = 10; // Replace with expected count

            _mockPostsRepository.Setup(repo => repo.getPostLikeCount(postId)).Returns(expectedCount);

            // Act
            var count = _postsService.getPostLikeCount(postId);

            // Assert
            Assert.AreEqual(expectedCount, count);
        }
    }
}
