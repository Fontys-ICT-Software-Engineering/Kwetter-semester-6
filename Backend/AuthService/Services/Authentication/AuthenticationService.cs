using AuthService.Data;
using AuthService.DTOs;
using AuthService.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {

        private readonly DataContext _dataContext;

        public AuthenticationService(DataContext context)
        {
            _dataContext = context;
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


    }
}
