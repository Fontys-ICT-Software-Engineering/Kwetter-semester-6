using AuthService.Data;
using AuthService.DTOs;
using AuthService.Models;
using BCrypt.Net;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;

namespace AuthService.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {

        private readonly DataContext _dataContext;
        private readonly IConfiguration _configuration;
        private readonly IPublishEndpoint _publishEndpoint;

        public AuthenticationService(DataContext context, IConfiguration configuration, IPublishEndpoint endpoint)
        {
            _dataContext = context;
            _configuration = configuration;
            _publishEndpoint = endpoint;
        }

        public async Task<List<UserDTO>> GetAllUsers()
        {
            List<UserDTO> users = new List<UserDTO>();

            try
            {
                List<User> userlist = await _dataContext.users.ToListAsync();

                foreach (User user in userlist)
                {
                    users.Add(new UserDTO(user.Email));
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return users;
        }

        public async Task<CreateUserDTO> Register(CreateUserDTO register)
        {
            foreach (PropertyInfo pi in register.GetType().GetProperties())
            {
                if (pi.PropertyType == typeof(string))
                {
                    string value = (string)pi.GetValue(register);
                    if (string.IsNullOrEmpty(value))
                    {
                        throw new InvalidOperationException("missing value");
                    }
                }
            }

            string password = BCrypt.Net.BCrypt.HashPassword(register.Password);
            Guid id = Guid.NewGuid();

            User user = new User()
            {
                Id = id,
                Email = register.Email,
                Password = password,
                Role = UserRole.NORMAL,
                DateEnrolled = DateTime.Now
            };

            RegisterUserDTO profile = new RegisterUserDTO()
            {
                Name = register.Name,
                UserName = register.UserName,
                Adress = register.Adress,
                AuthId = id,
                Bio = register.Bio,
            };

            try
            {
                if (_dataContext.users.Any(u => u.Email == user.Email)) throw new ArgumentException("Email already exists");
  
                _dataContext.users.Add(user);
                await _publishEndpoint.Publish<RegisterUserDTO>(profile);

                _dataContext.SaveChanges();

            }
            catch (Exception ex)
            {
                throw;
            }
            return register;
        }

        public async Task<TokenDTO> GenerateToken(LoginDTO dto)
        {
            TokenDTO token = new TokenDTO();

            TokenManager manager = new TokenManager(_configuration);


            try
            {
                User user = await _dataContext.users.SingleAsync(u => u.Email == dto.Email);
                if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.Password)) throw new Exception("Email or passwords do not match");
                token.Token = manager.CreateToken(user).ToString();
            }
            catch(InvalidOperationException ex)
            {
                throw new Exception("User does not exist");
            }
            catch (Exception ex)
            {
                throw;
            }
            return token;
        }

    }
}
