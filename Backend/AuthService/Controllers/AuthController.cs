using AuthService.DTOs;
using AuthService.Models;
using AuthService.Services.Authentication;
using AuthService.Services.MessageProducer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RabbitMQ.Client;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace AuthService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authService;
        private readonly IConfiguration _configuration;
        private readonly IMessageProducer _messageProducer;
        
        public AuthController(IAuthenticationService authService, IConfiguration configuration, IMessageProducer messageProducer)
        {
            _authService = authService;
            _configuration = configuration;
            _messageProducer = messageProducer;
        }

        [HttpGet(Name = "GetAllUsers")]
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

        [HttpPost("/register", Name = "RegisterUser")]
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

        [HttpPost("/login", Name = "Login")]
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

        [HttpGet("/validate", Name = "ValidateUser")]
        [Authorize]
        public async Task<ActionResult<string>> validateUser()
        {
            return Ok("user Validated!");
        }

        [HttpGet("/validate/admin", Name = "ValidateAdmin")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<string>> validateAdmin()
        {
            return Ok("user Validated!");
        }

        [HttpGet("/rabbitMq")]
        public async Task<ActionResult<string>> RabbitMq()
        {
            UserDTO dto = new UserDTO("Fendamear");

            IConnectionFactory factory = new ConnectionFactory { HostName = "localhost", Port = 5672, UserName = "myuser", Password = "mypassword" };

            var conn = factory.CreateConnection();

            using var channel = conn.CreateModel();

            //channel.ExchangeDeclare("test", ExchangeType.Topic, true);

            //channel.QueueDeclare("Profile", durable: true, exclusive: true);

            //channel.QueueBind("Profile", ExchangeType.Topic, "Profile")

            var jsonString = JsonSerializer.Serialize(dto);
            var body = Encoding.UTF8.GetBytes(jsonString);

            channel.BasicPublish("", "Profile", body: body);

            return Ok("message sent");
        }
    }
}