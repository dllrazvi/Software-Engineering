using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client.models
{
    public class User
    {
        public Guid _id;

        public String? _username;

        public String? _profilePicturePath;

        public User(Guid id, String username, String profilePicturePath)
        {
            _id = id;
            _username = username;
            _profilePicturePath = profilePicturePath;
        }

        public Guid Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public String Username
        {
            get { return _username; }
            set { _username = value; }
        }

        public String ProfilePicturePath
        {
            get { return _profilePicturePath; }
            set { _profilePicturePath = value; }
        }

        public override string ToString()
        {
            return _username;
        }
    }
}
