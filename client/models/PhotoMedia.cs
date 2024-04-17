namespace client.models
{
    class PhotoMedia : Media
    {
        public PhotoMedia(string filePath, string ext) :
            base(filePath, ext)
        {

        }
		public PhotoMedia(string filePath) :
		   base(filePath)
		{

		}
	}
}
