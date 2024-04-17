using System.IO;

namespace client.models
{
    class VideoMedia : Media
    {
        public VideoMedia(string filePath, string ext) :
            base(filePath, Path.GetExtension(filePath))
        {
        }
		public VideoMedia(string filePath) :
			base(filePath)
		{
		}
	}
}
