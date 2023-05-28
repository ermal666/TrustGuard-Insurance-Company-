using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TrustGuard.Domain.Models;

namespace TrustGuard.Persistence.IRepositories
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
