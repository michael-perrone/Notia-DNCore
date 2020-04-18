using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Notia.Data;
using Notia.Dtos;
using Notia.Models;

namespace Notia.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;
        public AuthController(IAuthRepository repo, IConfiguration config) {
            _repo = repo;
            _config = config;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
        {
            userRegisterDto.Email = userRegisterDto.Email.ToLower();
            if (await _repo.UserExists(userRegisterDto.Email)) {
                return BadRequest("User already exists");
            }

            var userToCreate = new User 
            {   
                Email = userRegisterDto.Email,
                Name = userRegisterDto.Name
            };

            var createdUser = await _repo.Register(userToCreate, userRegisterDto.Password);
            
             var claims = new[] 
            {
                new Claim(ClaimTypes.NameIdentifier, createdUser.Id.ToString()),
                new Claim(ClaimTypes.Name, createdUser.Name),
                new Claim(ClaimTypes.Email, createdUser.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(100),
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new {
                token = tokenHandler.WriteToken(token)
            });
        }

        [HttpPost("login")]

        public async Task<IActionResult> Login(UserLoginDto userLoginDto) {
            var userLoggingIn = await _repo.Login(userLoginDto.Email.ToLower(), userLoginDto.Password);

            if (userLoggingIn == null) {
                return Unauthorized();
            }
            var claims = new[] 
            {
                new Claim(ClaimTypes.NameIdentifier, userLoggingIn.Id.ToString()),
                new Claim(ClaimTypes.Name, userLoggingIn.Name),
                new Claim(ClaimTypes.Email, userLoggingIn.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(100),
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new {
                token = tokenHandler.WriteToken(token)
            });
            
        }

    }
}