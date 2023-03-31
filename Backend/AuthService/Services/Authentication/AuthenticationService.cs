using AuthService.Data;
using AuthService.DTOs;
using AuthService.Models;
using BCrypt.Net;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {

        private readonly DataContext _dataContext;
        private readonly IConfiguration _configuration;

        public AuthenticationService(DataContext context, IConfiguration configuration)
        {
            _dataContext = context;
            _configuration = configuration;
        }

        public async Task<List<UserDTO>> GetAllUsers()
        {
            List<UserDTO> users = new List<UserDTO>();

            try
            {
                List<User> userlist = await _dataContext.users.ToListAsync();

                foreach (User user in userlist)
                {
                    users.Add(new UserDTO(user.UserName));
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
            string password = BCrypt.Net.BCrypt.HashPassword(register.Password);
            User user = new User(register.UserName, password, UserRole.NORMAL);

            try
            {
                if (_dataContext.users.Any(u => u.UserName == user.UserName)) throw new Exception();
                _dataContext.users.Add(user);
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
            User user = _dataContext.users.Single(u => u.UserName == dto.Username);
            TokenDTO token = new TokenDTO();

            TokenManager manager = new TokenManager(_configuration);

            try
            {

                //if (BCrypt.Net.BCrypt.Verify(dto.Password, user.Password)) throw new Exception();

                if (!user.Password.Equals(dto.Password)) throw new Exception();

                token.Token = manager.CreateToken(user).ToString();

            }
            catch (Exception ex)
            {
                throw;
            }
            return token;
        }

    }
}
