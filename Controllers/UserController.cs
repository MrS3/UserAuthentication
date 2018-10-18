using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UserAuth.API.Helpers;
using UserAuth.API.Models;
using UserAuth.API.Services;

namespace UserAuth.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController: ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public UserController(IUserService userService, IMapper mapper) 
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(UserDto userDto)
        {
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);

            try
            {
                var user = _mapper.Map<User>(userDto);
                var userToSave = await _userService.Register(user, userDto.Password);
                return Ok(new {
                    message = "User Registered",
                    user = _mapper.Map<UserDto>(userToSave)
                });
            
            } catch ( AppException ex)
            {
                return BadRequest( new {message = ex.Message});
            }
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> Login(string username, string password)
        {

        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.Users();
            return Ok(users);
        }
    }
}