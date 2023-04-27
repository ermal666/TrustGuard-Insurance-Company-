using System;
using HealthService.Models;
using System.Linq;
using System.Collections.Generic;

namespace HealthService.Data
{
    public class UserRepo : IUserRepo
    {
        private readonly AppDbContext _context;
        
        public UserRepo(AppDbContext context)
        {
            _context = context;
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public User GetUserById(int id)
        {
            return _context.Users.FirstOrDefault(c => c.Id == id);
        }

        public void CreateUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            _context.Users.Add(user);
        }
    }    
}
