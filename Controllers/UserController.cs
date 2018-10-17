using Microsoft.AspNetCore.Mvc;
using UserAuth.API.Services;

namespace UserAuth.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController: ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService) => _userService = userService;
    
    
    
    
    }
}