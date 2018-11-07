using System;
using System.Collections.Generic;
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
    [Authorize]
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
        
        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.Users();
            var mappedUsers = new List<UserDto>(); 
            foreach (User user in users)
            {
                mappedUsers.Add(_mapper.Map<UserDto>(user));
            }
            return Ok(mappedUsers);
        }
    }
}