using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using client.models;
using Microsoft.Data.SqlClient;

namespace client.repositories
{
    class UserRepository
    {
        private DatabaseConnection dbInstance;
        private SqlConnection conn;

        public UserRepository()
        {
            dbInstance = DatabaseConnection.Instance;
            conn = dbInstance.GetConnection();
        }
        public User getUserById(Guid id)
        {
            string query = "SELECT * FROM users WHERE user_id = @user_id";

            conn.Open();
            using (SqlCommand command = new SqlCommand(query, conn))
            {

                command.Parameters.AddWithValue("@user_id", id.ToString());

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Guid userId = new Guid(reader.GetString(0));
                        String username = reader.GetString(1);
                        String profilePicturePath = reader.GetString(2);
                        conn.Close();

                        return new User(userId, username, profilePicturePath);
                    }
                }
            }

            conn.Close();

            return null;
        }

        public List<User> getAllUsers()
        {
			string query = "SELECT * FROM users";
			List<User> users = new List<User>();
			conn.Open();
			using (SqlCommand command = new SqlCommand(query, conn))
			{



				using (SqlDataReader reader = command.ExecuteReader())
				{
                    
					while (reader.Read())
					{
						Guid userId = new Guid(reader.GetString(0));
						String username = reader.GetString(1);
						String profilePicturePath = reader.GetString(2);
						

						users.Add( new User(userId, username, profilePicturePath));
					}
				}
			}

			conn.Close();

			return users;
		}
    }
}
