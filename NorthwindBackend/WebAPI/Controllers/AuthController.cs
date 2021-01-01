using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login(UserForLoginDto userForLoginDto)
        {
            var loginResult = _authService.Login(userForLoginDto);
            if (!loginResult.Success)
            {
                return BadRequest(loginResult.Message);
            }
            var tokenResult = _authService.CreateAccessToken(loginResult.Data);
            if (tokenResult.Success)
            {
                return Ok(tokenResult.Data);
            }
            return BadRequest(tokenResult.Message);
        }

        [HttpPost("register")]
        public IActionResult Register(UserForRegisterDto userForRegisterDto)
        {
            var userExistResult = _authService.UserExist(userForRegisterDto.Email);
            if (!userExistResult.Success)
            {
                return BadRequest(userExistResult.Message);
            }
            var registerResult = _authService.Register(userForRegisterDto);
            if (!registerResult.Success)
            {
                return BadRequest(registerResult.Message);
            }
            var tokenResult = _authService.CreateAccessToken(registerResult.Data);
            if (tokenResult.Success)
            {
                return Ok(tokenResult.Data);
            }
            return BadRequest(tokenResult.Message);
        }
    }
}
