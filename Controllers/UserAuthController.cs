using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    public class UserAuthController: ControllerBase
    {
        private readonly IUserAuthorizationService _authService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        public UserAuthController(IUserAuthorizationService authService, IMapper mapper, IConfiguration config)
        {
            _authService = authService;
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
                var userToSave = await _authService.Register(user, userDto.Password);
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
        public async Task<IActionResult> Login(UserDto userDto)
        {
            try
            {
                var user = await _authService.Login(userDto.Name, userDto.Password);
                return Ok(new {Token = new TokenGenerator().Generate(user, _config)});

            } catch (AppException ex)
            {
                return BadRequest(new {message = ex.Message});
            }
        }
    }
}