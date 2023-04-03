using AuthService.DTOs;
using ProfileService.Data;
using ProfileService.DTOs;
using ProfileService.Models;

namespace ProfileService.Services
{
    public class ProfileServiceLayer : IProfileService
    {
        private readonly DataContext _dataContext;

        public ProfileServiceLayer(DataContext context) 
        { 
            _dataContext = context;
        }    

        public async Task RegisterUser(RegisterUserDTO dto)
        {
            Profile profile = new Profile()
            {
                Id = Guid.NewGuid(),
                AuthId = dto.AuthId,
                Adress = dto.Adress,
                Bio = dto.Bio,
                UserName = dto.UserName,
                Name = dto.Name,
            };

            try
            {

                if (_dataContext.Profiles.Any(u => u.AuthId == dto.AuthId)) throw new Exception();

                _dataContext.Profiles.Add(profile);
                await _dataContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
