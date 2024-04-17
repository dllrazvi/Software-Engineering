using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using client.models;
using System.Windows;

namespace client.modules
{
    internal class ConfigurationModule
    {

        private List<string> allowedFileExtensions;
        private int maxFileSize;
        private string GOOGLE_PLACES_API_KEY;
        private string ENCRYPTION_KEY;

        private string DB_IP = Environment.GetEnvironmentVariable("DB_IP");
        private string DB_USER = Environment.GetEnvironmentVariable("DB_USER");
        private string DB_PASS = Environment.GetEnvironmentVariable("DB_PASS");
        private string DB_SCHEMA = Environment.GetEnvironmentVariable("DB_SCHEMA");



        public List<string> getAllowedFileExtensions()
        {
            return this.allowedFileExtensions;
        }

        public int getMaxFileSize()
        {
            return this.maxFileSize;
        }

        public string getGOOGLE_PLACES_API_KEY()
        {
            return this.GOOGLE_PLACES_API_KEY;
        }

        public string getENCRYPTION_KEY()
        {
            return this.ENCRYPTION_KEY;
        }


        public ConfigurationModule()
        {
            FetchConfigurationValue();
        }

        private void FetchConfigurationValue()
        {
            DatabaseConnection dbInstance;
            SqlConnection conn;
            dbInstance = DatabaseConnection.Instance;
            conn = dbInstance.GetConnection();
            conn.Open();
            string query = "SELECT allowedFileExtensions, maxFileSize, GOOGLE_PLACES_API_KEY, ENCRYPTION_KEY FROM configurations";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        allowedFileExtensions = reader.GetString(0).Split(',').ToList();
                        maxFileSize = reader.GetInt32(1);
                        GOOGLE_PLACES_API_KEY = reader.GetString(2);
                        ENCRYPTION_KEY = reader.GetString(3);
                    }
                    else
                    {
                        MessageBox.Show("No configuration found.");
                    }
                }
            }
            conn.Close();
        }
    }
}
