using PropertyService.Models;

namespace PropertyService.Services
{
    public interface IUserService
    {
        List<User> GetUsers();
        User GetUser(int id);
        bool CreateUser(User user);

    }
}