using PropertyService.Models;
using System.Linq.Expressions;

namespace PropertyService.Data
{
    public interface IUserRepository
    {
        Task<bool> SaveChangesAsync();
        IQueryable<User> GetAllUsers();
        IQueryable<User> GetUserById(Expression<Func<User, bool>> expression);
        void CreateUser(User user);
        Task CreateUserAsync(User user);
        void CreateRange(List<User> users);
        Task CreateRangeAsync(List<User> users);
        void Delete(User user);
        void DeleteRange(List<User> users);
        void Update(User user);
        void UpdateRange(List<User> users);
    }
}