using AuthService.Authentication;
using AuthService.DTOs;
using AuthService.Models;
using AuthService.Services.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authService;
        private readonly IConfiguration _configuration;
        
        public AuthController(IAuthenticationService authService, IConfiguration configuration)
        {
            _authService = authService;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<ActionResult<string>> getAllUsers()
        {
            try
            {
                List<UserDTO> res = await _authService.GetAllUsers();
                if (res.Count == 0) return NotFound();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<CreateUserDTO>> createUser(CreateUserDTO dto)
        {
            try
            {
                CreateUserDTO res = await _authService.Register(dto);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("/login")]
        public async Task<ActionResult<TokenDTO>> Login(LoginDTO dto)
        {
            TokenDTO token = new TokenDTO();

            try
            {
                token = await _authService.GenerateToken(dto);
                if (token.Token == "") return BadRequest();
                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value!));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(1),
                    signingCredentials: cred
                    );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }




        [HttpGet("/validate")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<string>> validate()
        {
            return Ok("this works!");
        }
    }
}