using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ManagerHomeAPI.Data;
using ManagerHomeAPI.Dto;
using ManagerHomeAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ManagerHomeAPI.Controllers {
    [Route ("api/[controller]")]
    public class AuthController : Controller {
        private readonly IAuthResponsitory _repo;
        private readonly IConfiguration _config;
        public AuthController (IAuthResponsitory repo, IConfiguration config) {
            _config = config;
            _repo = repo;
        }

        [HttpPost ("register")]
        public async Task<IActionResult> Register ([FromBody] UserRegisterDto userRegisterDto) {
            if(string.IsNullOrEmpty(userRegisterDto.userName))
            userRegisterDto.userName = userRegisterDto.userName.ToLower ();
            
            if (await _repo.UserExists (userRegisterDto.userName))
                ModelState.AddModelError ("Username", "Username has exits");

            //validate request
            if (!ModelState.IsValid) {
                return BadRequest (ModelState);
            }

            var userToCreate = new User {
                userName = userRegisterDto.userName,
                firstName = userRegisterDto.firstName,
                lastName = userRegisterDto.lastName,
            };
            var createUser = await _repo.Register (userToCreate, userRegisterDto.Password);
            return StatusCode (201);
        }
        [HttpPost("login")]
        public async Task<IActionResult> login([FromBody] UserLoginDto userLoginDto){
            
            var userFromRepo = await _repo.Login(userLoginDto.username.ToLower(), userLoginDto.password);
            if(userFromRepo == null){
                return Unauthorized();
            }
            
            //generate token
            var tokenHandeler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config.GetSection("AppSettings:Token").Value);
            var tokenDescriptor = new SecurityTokenDescriptor{
                Subject = new  ClaimsIdentity( new Claim[]{
                    new Claim (ClaimTypes.NameIdentifier, userFromRepo.id.ToString()),
                    new Claim(ClaimTypes.Name, userFromRepo.userName)
                }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey (key),
                SecurityAlgorithms.HmacSha512Signature)
                
            };
            var token = tokenHandeler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandeler.WriteToken(token);
            return Ok(new {tokenString});
        }
    }

}