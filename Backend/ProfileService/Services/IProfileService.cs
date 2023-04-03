using AuthService.DTOs;
using ProfileService.DTOs;

namespace ProfileService.Services
{
    public interface IProfileService
    {
        public Task RegisterUser(RegisterUserDTO dto);
    }
}
