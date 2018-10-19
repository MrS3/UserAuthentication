using System.Threading.Tasks;
using UserAuth.API.Models;

namespace UserAuth.API.Services
{
    public interface IUserAuthorizationService
    {
        Task<User> Register(User user, string password);
        Task<User> Login(string username, string passsword); 
    }
}