using client.models;
using client.repositories;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client.tests
{
    [TestFixture]
    internal class PostsRepositoryTests
    {
        private PostsRepository _postsRepository;

        [SetUp]
        public void Setup()
        {
            this._postsRepository = new PostsRepository();
        }

        [Test]
        public void AddPost_ValidPost_ReturnsTrue()
        {
            var post = new Post(Guid.NewGuid(), "Test Description", Guid.Parse("D6666B72-7E1F-4A8D-9226-38F6DA717A77"), new List<Guid>(), Guid.NewGuid(), Guid.NewGuid(), new Media("C:\\Users\\Raul\\Desktop\\florin.jpg"), 0, "ChIJ-YqOoRUMSUcRoBLP7U19Ncw", DateTime.Now);

            var result = this._postsRepository.addPostToDB(post);

            Assert.IsTrue(result);
        }

        [Test]
        public void AddPost_InvalidPost_ReturnsFalse()
        {
            try
            {
                var post = new Post(Guid.NewGuid(), "Test Description", Guid.Parse("asdas"), new List<Guid>(), Guid.NewGuid(), Guid.NewGuid(), new Media("C:\\Users\\Raul\\Desktop\\floasdrin.jpg"), 0, "ChIJ-YqOoRUMSUcRoBLP7U19Ncw", DateTime.Now);

                var result = this._postsRepository.addPostToDB(post);
            }
            catch (Exception e) { Assert.Pass(); }
            Assert.Fail();

        }

        [Test]
        public void RemovePost_PostExists_ReturnTrue()
        {
            String postID = Guid.NewGuid().ToString();
            var post = new Post(Guid.Parse(postID), "Test Description", Guid.Parse("D6666B72-7E1F-4A8D-9226-38F6DA717A77"), new List<Guid>(), Guid.NewGuid(), Guid.NewGuid(), new Media("C:\\Users\\Raul\\Desktop\\florin.jpg"), 0, "ChIJ-YqOoRUMSUcRoBLP7U19Ncw", DateTime.Now);

            this._postsRepository.addPostToDB(post);

            var result = this._postsRepository.removePostFromDB(Guid.Parse(postID));

            Assert.IsTrue(result);

        }

        [Test]
        public void RemovePost_PostDoesNotExist_ReturnFalse()
        {
            try
            {
                String postID = Guid.NewGuid().ToString();
                var post = new Post(Guid.Parse(postID), "Test Description", Guid.Parse("asdasd"), new List<Guid>(), Guid.NewGuid(), Guid.NewGuid(), new Media("C:\\Users\\Raul\\Desktop\\florin.jpg"), 0, "ChIJ-YqOoRUMSUcRoBLP7U19Ncw", DateTime.Now);

                this._postsRepository.addPostToDB(post);

                var result = this._postsRepository.removePostFromDB(Guid.NewGuid());
            }
            catch(Exception e) { Assert.Pass(); }
            Assert.Fail();
        }

        [Test]
        public void AddReactionToPost_ValidReaction_ReturnsTrue()
        {
            var postId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var reactionType = 1; 
            var result = this._postsRepository.addReactionToPost(postId, userId, reactionType);
            Assert.IsTrue(result);
        }

        [Test]
        public void AddReactionToPost_InvalidReaction_ReturnsFalse()
        {
            try {
                var postId = Guid.Parse("8EB731DD-07DC-41DB-A576-6AB50C326EA4");
                var userId = Guid.Parse("D6672-7E1F-4A8D-9226-38F6DA717A77");
                var reactionType = 100;
                var result = this._postsRepository.addReactionToPost(postId, userId, reactionType);
            }
            catch(Exception e)
            {
                Assert.Pass();
            }

            Assert.Fail();
        }

        [Test]
        public void AddMentionToPost_ValidMention_ReturnsTrue()
        {
            var postId = Guid.Parse("8EB731DD-07DC-41DB-A576-6AB50C326EA4");
            var userId = Guid.Parse("D6666B72-7E1F-4A8D-9226-38F6DA717A77");
            var result = this._postsRepository.addMentionToPost(postId, userId);
            Assert.IsTrue(result);
        }

        [Test]
        public void AddMentionToPost_InvalidMention_ReturnsFalse()
        {
            try
            {
                var postId = Guid.Parse("8ASD");
                var userId = Guid.Parse("DASD");
                var result = this._postsRepository.addMentionToPost(postId, userId);

            }
            catch (Exception e)
            {
                Assert.Pass();
            }

            Assert.Fail();
        }

        [Test]
        public void RemoveReactionToPost_ValidRemoval_ReturnsTrue()
        {
            var postId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            this._postsRepository.addReactionToPost(postId, userId, 1);
            var result = this._postsRepository.removeReactionToPost(postId, userId);
            Assert.IsTrue(result);
        }

        [Test]
        public void RemoveReactionToPost_InvalidRemoval_ReturnsFalse()
        {
            try
            {
                var postId = Guid.Parse("8ASD");
                var userId = Guid.Parse("DASD");
                var result = this._postsRepository.removeReactionToPost(postId, userId);
                Assert.IsTrue(result);

            }
            catch (Exception e)
            {
                Assert.Pass();
            }

            Assert.Fail();
        }

        [Test]
        public void UpdatePostDescription_ValidUpdate_ReturnsTrue()
        {
            var postId = Guid.NewGuid();
            var newDescription = "Updated Description";
            var result = this._postsRepository.updatePostDescription(postId, newDescription);
            Assert.IsTrue(result);
        }

        [Test]
        public void UpdatePostDescription_InvalidUpdate_ReturnFalse()
        {
            try
            {
                var postId = Guid.Parse("aSDASD");
                var newDescription = "Updated Description";
                var result = this._postsRepository.updatePostDescription(postId, newDescription);
                Assert.IsTrue(result);
            }
            catch (Exception e)
            {
                Assert.Pass();
            }

            Assert.Fail();
        }

        [Test]
        public void UpdatePostLocation_ValidUpdate_ReturnsTrue()
        {
            var postId = Guid.NewGuid();
            var newLocationID = "New Location ID";
            var result = this._postsRepository.updatePostLocation(postId, newLocationID);
            Assert.IsTrue(result);
        }

        [Test]
        public void UpdatePostLocation_InvalidUpdate_ReturnsFalse()
        {
            try
            {
                var postId = Guid.Parse("Asdasd");
                var newLocationID = "New Location ID";
                var result = this._postsRepository.updatePostLocation(postId, newLocationID);
                Assert.IsTrue(result);
            }
            catch (Exception e)
            {
                Assert.Pass();
            }

            Assert.Fail();
        }

        [Test]
        public void GetAllPosts_ReturnsListOfPosts()
        {
            var posts = this._postsRepository.getAllPosts();
            Assert.IsNotNull(posts);
            Assert.IsInstanceOf<List<Post>>(posts);
        }

        [Test]
        public void GetAllPostsFromLocation_ValidLocation_ReturnsListOfPosts()
        {
            var locationID = "valid_location_id";
            var posts = this._postsRepository.getAllPostsFromLocation(locationID);
            Assert.IsNotNull(posts);
            Assert.IsInstanceOf<List<Post>>(posts);
        }

        [Test]
        public void GetPostById_ValidPostId_ReturnsPost()
        {
            var postId = Guid.Parse("8F8A147A-5B80-47E9-A092-A675F439A515");
            var post = this._postsRepository.getPostById(postId);
            Assert.IsNotNull(post);
            Assert.IsInstanceOf<Post>(post);
        }

        [Test]
        public void GetPostLikeCount_ValidPostId_ReturnsCount()
        {
            var postId = Guid.NewGuid();
            var count = this._postsRepository.getPostLikeCount(postId);
            Assert.IsInstanceOf<int>(count);
        }

    }
}
