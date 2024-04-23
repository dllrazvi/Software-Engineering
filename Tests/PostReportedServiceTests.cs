using NUnit.Framework;
using Moq;
using System;
using System.Collections.Generic;
using client.models;
using client.repositories;
using client.services;

[TestFixture]
public class PostReportedServiceTests
{
    private Mock<IPostReportedRepository> _repositoryMock;
    private PostReportedService _postReportedService;

    [SetUp]
    public void Setup()
    {
        _repositoryMock = new Mock<IPostReportedRepository>();
        _postReportedService = new PostReportedService(_repositoryMock.Object);
    }

    [Test]
    public void AddPostReported_ValidPostReported_ReturnsTrue()
    {
        // Arrange
        var postReported = new PostReported(
            Guid.NewGuid(),
            "Test Reason",
            "Test Description",
            Guid.NewGuid(),
            Guid.NewGuid()
        );

        _repositoryMock.Setup(repo => repo.addReportedPostToDB(It.IsAny<PostReported>())).Returns(true);

        // Act
        bool result = _postReportedService.addPostReported(postReported);

        // Assert
        Assert.IsTrue(result, "Adding reported post failed.");
    }

    [Test]
    public void RemovePostReported_ValidPostReported_ReturnsTrue()
    {
        // Arrange
        var postReported = new PostReported(
            Guid.NewGuid(),
            "Test Reason",
            "Test Description",
            Guid.NewGuid(),
            Guid.NewGuid()
        );

        _repositoryMock.Setup(repo => repo.removeReportedPostFromDB(It.IsAny<PostReported>())).Returns(true);

        // Act
        bool result = _postReportedService.removePostReported(postReported);

        // Assert
        Assert.IsTrue(result, "Removing reported post failed.");
    }

    [Test]
    public void GetPostReportedList_ReturnsListOfPostReported()
    {
        // Arrange
        var expectedList = new List<PostReported>
        {
            new PostReported(Guid.NewGuid(), "Reason 1", "Description 1", Guid.NewGuid(), Guid.NewGuid()),
            new PostReported(Guid.NewGuid(), "Reason 2", "Description 2", Guid.NewGuid(), Guid.NewGuid())
        };

        _repositoryMock.Setup(repo => repo.getAll()).Returns(expectedList);

        // Act
        List<PostReported> resultList = _postReportedService.getPostReportedList();

        // Assert
        Assert.IsNotNull(resultList);
        Assert.AreEqual(expectedList.Count, resultList.Count);
        CollectionAssert.AreEqual(expectedList, resultList);
    }
}
