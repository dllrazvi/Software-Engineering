using client.repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using client.models;

namespace client.services
{
    class UserService
    {
        private UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User getUserById(Guid id)
        {
            return _userRepository.getUserById(id);
        }

        public List<User> getAllUsers()
        {
            return _userRepository.getAllUsers();
        }
    }
}
