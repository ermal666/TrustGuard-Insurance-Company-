using PropertyService.Data;
using PropertyService.Models;

namespace PropertyService.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public List<User> GetUsers()
        {
            var users = _userRepository.GetAllUsers().ToList();
            return users;
        }

        public User GetUser(int id)
        {
            var user = _userRepository.GetUserById(id);
            return user;
        }

        public bool CreateUser(User user)
        {
            return _userRepository.CreateUser(user);
        }
    }
}
