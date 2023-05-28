using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TrustGuard.Persistence.IRepositories;
using TrustGuard.Domain.Models;
using TrustGuard.Domain;

namespace TrustGuard.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;

        public UserRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> SaveChangesAsync()
        {
            var complete = await _dbContext.SaveChangesAsync() > 0;
            return complete;
        }

        public IQueryable<User> GetAllUsers()
        {
            return _dbContext.Set<User>();
        }

        public IQueryable<User> GetUserById(Expression<Func<User, bool>> expression)
        {

            return _dbContext.Set<User>().Where(expression);

        }

        public void CreateUser(User user)
        {
            _dbContext.Set<User>().Add(user);

        }

        public async Task CreateUserAsync(User user)
        {
            await _dbContext.Set<User>().AddAsync(user);

        }


        public void CreateRange(List<User> users)
        {
            _dbContext.Set<User>().AddRange(users);
        }

        public async Task CreateRangeAsync(List<User> users)
        {
            await _dbContext.Set<User>().AddRangeAsync(users);
        }

        public void Delete(User user)
        {
            _dbContext.Set<User>().Remove(user);
        }
        public void DeleteRange(List<User> users)
        {
            _dbContext.Set<User>().RemoveRange(users);
        }

        public void Update(User user)
        {
            _dbContext.Set<User>().Update(user);
        }
        public void UpdateRange(List<User> users)
        {
            _dbContext.Set<User>().UpdateRange(users);
        }
    }

}
