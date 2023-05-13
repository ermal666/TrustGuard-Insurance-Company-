using PropertyService.Models;

namespace PropertyService.Services
{
    public interface IUserService
    {
        Task<List<User>> GetUsersPaged(int pageIndex, int pageSize);
        Task<User> GetUser(int id);
        Task<bool> CreateUser(User user);
        Task DeleteUser(int id);

    }
}