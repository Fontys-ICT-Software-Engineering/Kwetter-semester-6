using AuthService.Data;
using AuthService.DTOs;
using AuthService.Models;
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
            User user = new User(register.UserName, register.Password, UserRole.NORMAL);

            try
            {
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

            TokenManager man = new TokenManager(_configuration);

            try
            {
                if (!user.Password.Equals(dto.Password)) throw new Exception();

                token.Token = man.CreateToken(dto.Username).ToString();

            }
            catch (Exception ex)
            {
                throw;
            }
            return token;
        }

    }
}
