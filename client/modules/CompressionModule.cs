using client.models;
using System.Windows;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace client.modules
{
    class CompressionModule
    {
        private long compressQuality = 60;
        private long originalQuality = 100;
        private String? tempPath;

        private void compressImage(Media photo)
        {
            varyQualityLevel(photo, compressQuality);
        }

        private void decompressImage(Media photo)
        {
            varyQualityLevel(photo, originalQuality);
        }

        private void varyQualityLevel(Media photo, long quality)
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

        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            foreach (ImageCodecInfo ici in encoders)
                if (ici.MimeType == mimeType) return ici;

            return null;
        }


        public void compressFile(Media mediaFile)
        {
            if (mediaFile.GetType() == typeof(PhotoMedia))
            {
                compressImage(mediaFile);
            }
        }

        public void decompressFile(Media mediaFile)
        {
            if (mediaFile.GetType() == typeof(PhotoMedia))
            {
                decompressImage(mediaFile);
            }
        }
    }
}
