using client.models;
using client.modules;
using client.services;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace client.repositories
{
    internal class HashtagRepository
    {
        private DatabaseConnection dbInstance;
        private SqlConnection conn;
        public HashtagRepository()
        {
            dbInstance = DatabaseConnection.Instance;
            conn = dbInstance.GetConnection();
        }

        public bool addHashtagPost(String postId, String hashtagId)
        {
            string query = "insert into post_hashtags values (@post_id, @hashtag_id)";
            conn.Open();

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@hashtag_id", hashtagId);
                cmd.Parameters.AddWithValue("@post_id", postId);

                try
                {
                    int rowsAffected = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                    conn.Close();
                    return false;
                }
            }
            conn.Close();
            return true;
        }

        public bool addHashtag(Hashtag hashtag)
        {
            string query = "insert into hashtags values (@hashtag_id, @hashtag_name)";
            conn.Open();

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@hashtag_id", hashtag.Id);
                cmd.Parameters.AddWithValue("@hashtag_name", hashtag.Name);

                try
                {
                    int rowsAffected = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                    conn.Close();
                    return false;
                }
            }
            conn.Close();
            return true;
        }

        public bool removeHashtag(String id)
        {
            string query = "delete from hashtags where hashtag_id = @hashtag_id";
            conn.Open();

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@hashtag_id", id);

                try
                {
                    int rowsAffected = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                    conn.Close();
                    return false;
                }
            }
            conn.Close();
            return true;
        }

        public bool removePostFromHashtag(String postId, String hashtagId)
        {
            string query = "delete from post_hashtags where hashtag_id = @hashtag_id and post_id = @post_id";
            conn.Open();

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@hashtag_id", hashtagId);
                cmd.Parameters.AddWithValue("@post_id", postId);

                try
                {
                    int rowsAffected = cmd.ExecuteNonQuery();
                    conn.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                    conn.Close();
                    return false;
                }
            }
		}

        public List<Post> getAllPosts(String hashtagId)
        {
            string query = "select p.* from post_hashtags ph inner join posts p on p.post_id = ph.post_id where hashtag_id = @hashtag_id";
            conn.Open();
            List<Post> posts = new List<Post>();

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@hashtag_id", hashtagId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        Guid post_id = Guid.Parse(reader.GetString(0).ToUpper());
                        Guid owner_user_id = Guid.Parse(reader.GetString(1));
                        String? description = reader.IsDBNull(2) ? null : reader.GetString(2);
                        Guid commented_post_id = reader.IsDBNull(3) ? Guid.Empty : Guid.Parse(reader.GetString(3));
                        Guid original_post_id = reader.IsDBNull(4) ? Guid.Empty : Guid.Parse(reader.GetString(4));
                        String? media_path = reader.IsDBNull(5) ? null : reader.GetString(5);
                        int post_type = reader.GetInt16(6);
                        String? location_id = reader.IsDBNull(7) ? null : reader.GetString(7);
                        DateTime created_date = reader.GetDateTime(8);





                        List<Guid> metionedUsers = new List<Guid>();
                        String getMentionedUsers = "SELECT user_id FROM mentions WHERE post_id = @post_id";
                        using (SqlCommand command2 = new SqlCommand(getMentionedUsers, conn))
                        {


                            command2.Parameters.AddWithValue("@post_id", post_id);
                            using (SqlDataReader reader2 = command2.ExecuteReader())
                            {
                                while (reader2.Read())
                                {
                                    metionedUsers.Add(Guid.Parse(reader2.GetString(0)));
                                }
                            }
                        }


                        Post post;

                        if (post_type == 0)
                        {
                            post = new Post(post_id, description, owner_user_id, metionedUsers, commented_post_id, original_post_id, null, post_type, location_id, created_date);
                            posts.Add(post);
                        }

                        else if (post_type == 1)
                        {

                            post = new Post(post_id, description, owner_user_id, metionedUsers, commented_post_id, original_post_id, new PhotoMedia(media_path), post_type, location_id, created_date);
                            posts.Add(post);
                        }
                        else if (post_type == 2)
                        {
                            post = new Post(post_id, description, owner_user_id, metionedUsers, commented_post_id, original_post_id, new VideoMedia(media_path), post_type, location_id, created_date);
                            posts.Add(post);
                        }


                    }
                }
            }
            conn.Close();
            return posts;
        }

        public Hashtag searchHashtag(String name)
        {
            string query = "SELECT * FROM hashtags WHERE name = @hashtag_name";

            conn.Open();
            using (SqlCommand command = new SqlCommand(query, conn))
            {

                command.Parameters.AddWithValue("@hashtag_name", name);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Guid hashtag_id = Guid.Parse(reader.GetString(0));
                        String hashtag_name = reader.GetString(1);
                        conn.Close();
                        return new Hashtag(hashtag_id, hashtag_name);
                    }
                }
            }
            conn.Close();
            return null;
        }

        public bool searchPostHashtagPair(String postId, string hashtagId)
        {
            string query = "select count(*) from post_hashtags where hashtag_id = @hashtag_id and post_id = @post_id";
            conn.Open();

            using (SqlCommand command = new SqlCommand(query, conn))
            {
                command.Parameters.AddWithValue("@hashtag_id", hashtagId);
                command.Parameters.AddWithValue("@post_id", postId);

                int count = (int)command.ExecuteScalar();

                if (count > 0)
                {
                    conn.Close();
                    return true;
                }

            }
            conn.Close();
            return false;
        }
    }
}
