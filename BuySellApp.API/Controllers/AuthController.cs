using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BuySellApp.API.DTOs;
using BuySellApp.API.Interfaces;
using BuySellApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BuySellApp.API.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase {
        public IAuthRepository _authRepo { get; }
        public IConfiguration _config { get; }
        public AuthController (IAuthRepository authRepo, IConfiguration config) {
            _config = config;
            _authRepo = authRepo;
        }

        [HttpPost ("register")]
        public async Task<IActionResult> Register (RegisterDTO userRegisterDTO) {
            userRegisterDTO.username = userRegisterDTO.username.ToLower ();
            if (await _authRepo.UserExists (userRegisterDTO.username)) {
                return BadRequest ("Username already exists");
            }
            var userTobeCreated = new User {
                Username = userRegisterDTO.username
            };

            var createUser = await _authRepo.Register (userTobeCreated, userRegisterDTO.password);

            return StatusCode (201);
        }

        [HttpPost ("login")]
        [HttpOptions]
        public async Task<IActionResult> Login (LoginDTO userLoginDTO) {

            var checkUser = await _authRepo.Login (userLoginDTO.Username.ToLower (), userLoginDTO.Password);

            if (checkUser == null) {
                return Unauthorized ();
            }

            var claims = new [] {
                new Claim (ClaimTypes.NameIdentifier, checkUser.Id.ToString ()),
                new Claim (ClaimTypes.Name, checkUser.Username)
            };

            var key = new SymmetricSecurityKey (Encoding.UTF8.GetBytes (_config.GetSection ("AppSettings:Token").Value));

            var creds = new SigningCredentials (key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity (claims),
                Expires = DateTime.Now.AddDays (1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler ();

            var token = tokenHandler.CreateToken (tokenDescriptor);

            return Ok (new {
                token = tokenHandler.WriteToken (token)
            });
        }
    }
}