using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace client.models
{   
   
    public class Post
    {

        public Guid id { get; set; }
        public String? description { get; set; }
        public Guid ownerUserID {  get; set; }
        public List<Guid> mentionedUsers { get; set; }
        public Guid commentedPostID {  get; set; }
        public Guid originalPostID { get; set; }
        public Media? media { get; set; }
        public int postType { get; set; }
        public String? locationID { get; set; }
        public DateTime createdDate { get; set; }

        public User? user { get; set; }
		public List<String>? mentionedUsersUsernames { get; set; }

        public int LikesCount { get; set; }
        public String? locationName { get; set; }

		public Post(Guid _id,String? _description, Guid _onwerUserID, List<Guid> _mentionedUsers, Guid _commentedPostID, Guid _orignalPostID, Media? _media, int _postType, String? _locationID, DateTime _createdDate)
		{
            id = _id;
            description = _description;
            ownerUserID = _onwerUserID;
            mentionedUsers = _mentionedUsers;
            commentedPostID =   _commentedPostID;
            originalPostID = _orignalPostID;
            media = _media;
            postType = _postType;
            locationID = _locationID;
            createdDate = _createdDate;

		}
        public override string ToString()
        {
            return $"Post : {id}\n{description}\n{ownerUserID}\n{mentionedUsers}\n{commentedPostID}\n{originalPostID}\n{media}\n{postType}\n{locationID}\n{createdDate}";
        }
    }
}
