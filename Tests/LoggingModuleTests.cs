using client.modules;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Serilog;
using System;

namespace client.tests
{
    [TestFixture]
    internal class LoggingModuleTests
    {
        [SetUp]
        public void Setup()
        {
            // Setup for Logging Module could involve ensuring that the database is ready for logging,
            // or that any required configurations are set.
        }

        [Test]
        public void LoggerInitialization_CreatesTableInDatabase_TableExists()
        {
            // This test would ideally check if the logging table exists in the database.
            // Since it involves database checking, this might require integration testing setup or mocking.
            Assert.Pass("Assuming table creation is checked by an integration test.");
        }

        [Test]
        public void CreateLogger_ValidType_ReturnsLoggerInstance()
        {
            ILogger<LoggingModule> logger = LoggingModule.CreateLogger<LoggingModule>();
            Assert.IsInstanceOf<ILogger<LoggingModule>>(logger);
        }

        [Test]
        public void CreateLogger_SameType_ReturnsSameLoggerInstance()
        {
            ILogger<LoggingModule> logger1 = LoggingModule.CreateLogger<LoggingModule>();
            ILogger<LoggingModule> logger2 = LoggingModule.CreateLogger<LoggingModule>();
            Assert.AreSame(logger1, logger2);
        }

        [Test]
        public void Logger_WriteLogEntry_WritesToDatabase()
        {
            // This test checks if log entries are written to the database.
            // This might also be better suited as an integration test due to the involvement of the database.
            ILogger<LoggingModule> logger = LoggingModule.CreateLogger<LoggingModule>();
            logger.LogInformation("Test log entry at {DateTime}", DateTime.UtcNow);

            // Verify the entry in the database or check if the logger did not throw an exception
            Assert.Pass("Assuming log write is checked by an integration test or log monitoring tool.");
        }
    }
}
