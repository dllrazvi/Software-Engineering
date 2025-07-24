using NUnit.Framework;
using client.modules;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace client.tests
{
    [TestFixture]
    internal class ConfigurationModuleTests
    {
        private ConfigurationModule _configurationModule;

        [SetUp]
        public void Setup()
        {
            _configurationModule = new ConfigurationModule();
        }

        [Test]
        public void FetchConfigurationValue_ValidConfigurations_RetrievesValues()
        {
            // Act
            //_configurationModule.FetchConfigurationValue();

            // Assert
            Assert.IsNotNull(_configurationModule.getAllowedFileExtensions());
            Assert.Greater(_configurationModule.getAllowedFileExtensions().Count, 0);
            Assert.Greater(_configurationModule.getMaxFileSize(), 0);
            //Assert.IsNotNullOrEmpty(_configurationModule.getGOOGLE_PLACES_API_KEY());
            //Assert.IsNotNullOrEmpty(_configurationModule.getENCRYPTION_KEY());
        }

        [Test]
        public void FetchConfigurationValue_InvalidConfigurations_PopsMessageBox()
        {
            // Arrange
            var previousOutput = Console.Out;
            var writer = new StringWriter();
            Console.SetOut(writer);

            // Act
            //_configurationModule.FetchConfigurationValue();

            // Assert
            var output = writer.GetStringBuilder().ToString().Trim();
            Assert.AreEqual("No configuration found.", output);

            // Restore Console.Out
            Console.SetOut(previousOutput);
        }
    }
}
