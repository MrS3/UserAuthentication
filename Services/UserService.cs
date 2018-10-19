using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserAuth.API.Helpers;
using UserAuth.API.Models;

namespace UserAuth.API.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;
        public UserService(DataContext context) => _context = context;

        public void Add<T>(T entity) where T : class
        {
            throw new System.NotImplementedException();
        }

        public void Delete<T>(T entity) where T : class
        {
            throw new System.NotImplementedException();
        }
        
        public async Task<List<User>> Users() => await _context.Users.ToListAsync();
    }
}