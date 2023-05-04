using PropertyService.Models;

namespace PropertyService.Data
{
    public interface IUserRepository
    {
        bool SaveChanges();
        IQueryable<User> GetAllUsers();
        User GetUserById(int id);
        bool CreateUser(User user);
    }
}