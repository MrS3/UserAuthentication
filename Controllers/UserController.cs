using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
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
        private readonly IConfiguration _config;
        public UserController(IUserService userService, IMapper mapper, IConfiguration config) 
        {
            _userService = userService;
            _mapper = mapper;
            _config = config;
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
            try
            {
                var user = await _userService.Login(username, password);
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_config.GetSection("AppSettings:Secret").Value);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                   Subject = new ClaimsIdentity(new Claim[]
                   {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.Name)
                    }),
                    Expires = DateTime.Now.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token); 
                return Ok(new {tokenString, user});

            } catch (AppException ex)
            {
                return BadRequest(new {message = ex.Message});
            }
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.Users();
            return Ok(users);
        }
    }
}