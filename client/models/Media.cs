using System.IO;
using System.Windows;
using client.modules;

namespace client.models
{
    public class Media
    {
        private readonly ValidationModule validationModule;
        public String FilePath { get; set; }

        public Media(String filePath, String fileExtension)
        {
            validationModule = new ValidationModule();
            try
            {
                validationModule.ValidateFile(filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(filePath);
                throw new Exception("Invalid file path!");
            }

            string workingDirectory = Environment.CurrentDirectory;
            Guid myuuid = Guid.NewGuid();
            string myuuidAsString = myuuid.ToString();

            String newFilePath = workingDirectory + @"\" + myuuidAsString + fileExtension;
            File.Copy(filePath, newFilePath);

            this.FilePath = newFilePath;
        }

        public Media(String filePath)
        {
            this.FilePath = filePath;
        }
    }
}
