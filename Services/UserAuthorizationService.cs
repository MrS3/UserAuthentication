using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserAuth.API.Helpers;
using UserAuth.API.Models;

namespace UserAuth.API.Services
{
    public class UserAuthorizationService
    {
        private readonly DataContext _context;
        public UserAuthorizationService(DataContext context) => _context = context;

        public async Task<User> Login(string username, string passsword)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Name == username);
             if (user == null || !PasswordHash.VerifyPassword(passsword, user.PasswordSalt, user.PasswordHash))
                throw new AppException("Password or Login incorrect");
           return user;
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
    }
}