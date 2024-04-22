using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;
using client.models;
using client.repositories;
using System;
using System.Collections.Generic;

namespace client.tests
{
    [TestFixture]
    public class PostSavedRepositoryTests
    {
        private IPostSavedRepository _postSavedRepository;

        [SetUp]
        public void Setup()
        {
            _postSavedRepository = new PostSavedRepository();
        }

        [Test]
        public void AddPostSavedtoDB_ValidPostSaved_ReturnsTrue()
        {
            // Arrange
            var postSaved = new PostSaved(Guid.NewGuid(), 
                                          Guid.Parse("8EB731DD-07DC-41DB-A576-6AB50C326EA4"),
                                          Guid.Parse("D6666B72-7E1F-4A8D-9226-38F6DA717A77"));
            // Act
            bool result = _postSavedRepository.addPostSavedtoDB(postSaved);

            // Assert
            Assert.IsTrue(result, "Adding post saved failed.");
        }

        [Test]
        public void RemovePostSavedFromDB_ValidPostSaved_ReturnsTrue()
        {
            // Arrange
            PostSaved postSaved = new PostSaved(
                Guid.NewGuid(), // Generate a unique save_id
                Guid.NewGuid(), // Generate a unique post_id
                Guid.NewGuid()  // Generate a unique user_id
            );

            _postSavedRepository.addPostSavedtoDB(postSaved);

            // Act
            bool result = _postSavedRepository.removePostSavedFromDB(postSaved);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void GetAll_ReturnsListOfPostSaved()
        {
            // Arrange
            List<PostSaved> postSavedList = new List<PostSaved>();

            // Act
            postSavedList = _postSavedRepository.getAll();

            // Assert
            Assert.IsNotNull(postSavedList);
            Assert.IsInstanceOf<List<PostSaved>>(postSavedList);
        }
    }
}
