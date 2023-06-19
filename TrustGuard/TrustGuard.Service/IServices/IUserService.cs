using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrustGuard.Domain.Models;

namespace TrustGuard.Service.IServices
{
    public interface IUserService
    {
        Task<List<User>> GetUsersPaged(int pageIndex, int pageSize);
        Task<User> GetUser(string id);
        Task<bool> CreateUser(User user);
        Task DeleteUser(int id);

    }
}
