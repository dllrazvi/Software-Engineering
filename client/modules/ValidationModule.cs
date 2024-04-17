using Microsoft.Data.SqlClient;
using System.IO;

namespace client.modules
{
    internal class ValidationModule
    {

        private DatabaseConnection dbInstance;
        private SqlConnection conn;


        private String[] allowedFileExtensions;
        private readonly long maxSizeInBytes;
        public ValidationModule()
        {
            dbInstance = DatabaseConnection.Instance;
            conn = dbInstance.GetConnection();

            conn.Close();
            conn.Open();

            String query = "Select * from configurations";

            using (SqlCommand command = new SqlCommand(query, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        allowedFileExtensions = reader.GetString(0).Split(" ");
                        maxSizeInBytes = reader.GetInt32(1) * 1024 * 1024;
                    }
                }
            }
            conn.Close();
        }

        public void ValidateFile(String filePath)
        {

            try
            {
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException("File does not exist.", filePath);
                }

                FileInfo fileInfo = new FileInfo(filePath);

                // Check file size
                if (fileInfo.Length > maxSizeInBytes)
                {
                    throw new FileLoadException("File size exceeds the maximum allowed size.", filePath);
                }

                // Check file type
                String fileExtension = Path.GetExtension(filePath);
                bool isValidType = false;

                foreach (String allowedType in allowedFileExtensions)
                {
                    if (fileExtension.Equals(allowedType, StringComparison.InvariantCultureIgnoreCase))
                    {
                        isValidType = true;
                        break;
                    }
                }

                if (!isValidType)
                {
                    throw new FileFormatException("Invalid file format.");
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"File not found: {ex.FileName}");
                throw;
            }
            catch (FileLoadException ex)
            {
                Console.WriteLine($"File load error: {ex.Message}");
                throw;
            }
            catch (FileFormatException ex)
            {
                Console.WriteLine($"Invalid file format: {ex.Message}");
                throw;
            }
        }
    }
}
