using System.Collections.Generic;
using System.Threading.Tasks;
using UserAuth.API.Models;

namespace UserAuth.API.Services
{
    public interface IUserService
    {
        Task<List<User>> Users();
        void Add<T>(T entity) where T: class;
        void Delete<T>(T entity) where T: class;

    }
}