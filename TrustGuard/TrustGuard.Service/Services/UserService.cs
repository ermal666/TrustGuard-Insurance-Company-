﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrustGuard.Domain.Models;
using TrustGuard.Persistence.IRepositories;
using TrustGuard.Service.IServices;

namespace TrustGuard.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<User>> GetUsersPaged(int pageIndex, int pageSize)
        {
            var users = await _userRepository.GetAllUsers()
                                                    .OrderByDescending(x => x.Id)
                                                    .Skip((pageIndex - 1) * pageSize)
                                                    .Take(pageSize)
                                                    .ToListAsync();
            return users;
        }

        public async Task<User> GetUser(string id)
        {
            var user = await _userRepository.GetUserById(x => x.Id == id.ToString()).FirstOrDefaultAsync();
            return user ?? new User();
        }

        public async Task DeleteUser(int id)
        {
            var user = await _userRepository.GetUserById(x => x.Id == id.ToString()).FirstOrDefaultAsync();
            _userRepository.Delete(user);
            await _userRepository.SaveChangesAsync();
        }

        public async Task<bool> CreateUser(User user)
        {
            await _userRepository.CreateUserAsync(user);
            var isAdded = await _userRepository.SaveChangesAsync();
            return isAdded;
        }

    }
}
