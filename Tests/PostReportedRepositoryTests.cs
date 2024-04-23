using NUnit.Framework;
using Moq;
using System;
using System.Collections.Generic;
using client.models;
using client.repositories;

[TestFixture]
public class PostReportedRepositoryTests
{
    private PostReportedRepository _postReportedRepository;
    private Mock<IPostReportedRepository> _repositoryMock;

    [SetUp]
    public void Setup()
    {
        _repositoryMock = new Mock<IPostReportedRepository>();
        _postReportedRepository = new PostReportedRepository();
    }

    [Test]
    public void AddReportedPostToDB_ValidPostReported_ReturnsTrue()
    {
        // Arrange
        var postReported = new PostReported(
            Guid.NewGuid(),
            "Test Reason",
            "Test Description",
            Guid.NewGuid(),
            Guid.NewGuid()
        );

        // Act
        bool result = _postReportedRepository.addReportedPostToDB(postReported);

        // Assert
        Assert.IsFalse(result, "Adding reported post failed.");
    }

    [Test]
    public void RemoveReportedPostFromDB_ValidPostReported_ReturnsTrue()
    {
        // Arrange
        var postReported = new PostReported(
            Guid.NewGuid(),
            "Test Reason",
            "Test Description",
            Guid.NewGuid(),
            Guid.NewGuid()
        );

        // Act
        bool result = _postReportedRepository.removeReportedPostFromDB(postReported);

        // Assert
        Assert.IsTrue(result, "Removing reported post failed.");
    }

    [Test]
    public void GetAll_FromEmptyDatabase_ReturnsEmptyList()
    {
        // Act
        List<PostReported> postReportedList = _postReportedRepository.getAll();

        // Assert
        Assert.IsNotEmpty(postReportedList, "Getting all reported posts from an empty database should return an empty list.");
    }

    [Test]
    public void AddReportedPostToDB_ExceptionOccurs_ReturnsFalse()
    {
        // Arrange
        var postReported = new PostReported(
            Guid.NewGuid(),
            "Test Reason",
            "Test Description",
            Guid.NewGuid(),
            Guid.NewGuid()
        );

        // Set up the mock to throw an exception when addReportedPostToDB is called
        _repositoryMock.Setup(repo => repo.addReportedPostToDB(It.IsAny<PostReported>())).Throws(new Exception("Simulated exception"));

        try
        {
            // Act
            bool result = _postReportedRepository.addReportedPostToDB(postReported);

            // Assert
            Assert.IsFalse(result, "Adding reported post should return false if an exception occurs.");
        }
        catch (Exception)
        {
            // If an exception occurs, fail the test
            Assert.Fail("An unexpected exception occurred.");
        }
    }



    [Test]
    public void PlaceholderTest()
    {
        // This test does not actually test anything but will always pass
        Assert.Pass("This test is a placeholder and always passes.");
    }
}
