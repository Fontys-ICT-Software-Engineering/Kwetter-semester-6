using AuthService.DTOs;

namespace AuthService.Services.Authentication
{
    public interface IAuthenticationService
    {
        public Task<List<UserDTO>> GetAllUsers();

        public Task<CreateUserDTO> Register(CreateUserDTO userDTO);

        public Task<TokenDTO> GenerateToken(LoginDTO dto);
    }
}
