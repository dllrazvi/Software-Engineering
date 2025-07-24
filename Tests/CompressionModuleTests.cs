using client.models;
using client.modules;
using NUnit.Framework;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Shell;

namespace client.tests
{
    [TestFixture]
    internal class CompressionModuleTests
    {
        private CompressionModule _compressionModule;
        private string _originalImagePath;
        private string _compressedImagePath;
        private string tempPath;

        [SetUp]
        public void Setup()
        {
            _compressionModule = new CompressionModule();

            // Assuming these are paths to valid image files for the purpose of testing.
            _originalImagePath = "path/to/original/image.jpg";
            _compressedImagePath = "path/to/compressed/image.jpg";

            // Create dummy image files to mimic compression and decompression.
            CreateDummyImage(_originalImagePath);
        }

        private void CreateDummyImage(string originalImagePath)
        {
            // You can create dummy image files here if needed.
            // This method can be implemented based on your specific requirements.
        }

        [Test]
        public void CompressFile_WithValidImage_CompressesImage()
        {
            // Arrange
            var photo = new PhotoMedia(_originalImagePath);
            string expectedOutputPath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.jpg");

            // Act
            _compressionModule.compressFile(photo);

            // Assert - Check if the output file exists.
            // This is an actual file system operation, making this an integration test, not a unit test.
            bool fileExists = File.Exists(expectedOutputPath);
            Assert.IsTrue(fileExists);

            // Cleanup
            if (fileExists)
            {
                File.Delete(expectedOutputPath);
            }
        }

        [Test]
        public void DecompressFile_WithValidImage_DecompressesImage()
        {
            // Arrange
            var photo = new PhotoMedia(_compressedImagePath);
            string expectedOutputPath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.jpg");

            // Act
            _compressionModule.decompressFile(photo);

            // Assert - Check if the output file exists.
            bool fileExists = File.Exists(expectedOutputPath);
            Assert.IsTrue(fileExists);

            // Cleanup
            if (fileExists)
            {
                File.Delete(expectedOutputPath);
            }
        }
        private void varyQualityLevel(Media photo, long quality)
        {
            try
            {
                using (Bitmap bmp1 = new Bitmap(photo.FilePath))
                {
                    ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);

                    System.Drawing.Imaging.Encoder myEncoder =
                        System.Drawing.Imaging.Encoder.Quality;

                    EncoderParameters myEncoderParameters = new EncoderParameters(1);

                    EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, quality);
                    myEncoderParameters.Param[0] = myEncoderParameter;

                    this.tempPath = Path.GetTempPath() + @"\" + Guid.NewGuid().ToString() + ".jpg";
                    bmp1.Save(tempPath, jpgEncoder, myEncoderParameters);
                }
                File.Delete(photo.FilePath);
                File.Copy(tempPath, photo.FilePath);
                File.Delete(tempPath);
            }
            catch (ArgumentException ex)
            {
                // Handle the invalid image file path or format error
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private ImageCodecInfo GetEncoder(ImageFormat jpeg)
        {
            throw new NotImplementedException();
        }
    }
}
