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

        public Task<User> Login(string username, string passsword)
        {
            throw new System.NotImplementedException();
        }

        public async Task<User> Register(User user, string password)
        {
            if (await _context.Users.AnyAsync( x => x.Name == user.Name))
                throw new AppException("Username " + user.Name + "already exist");

            var hashData = PasswordHash.CreatePasswordHash(password, user.PasswordHash, user.PasswordSalt);
            user.PasswordSalt = hashData.Item1;
            user.PasswordHash = hashData.Item2;
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<List<User>> Users() => await _context.Users.ToListAsync();
    }
}