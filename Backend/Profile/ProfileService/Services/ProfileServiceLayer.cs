using AuthService.DTOs;
using MassTransit;
using Microsoft.EntityFrameworkCore;
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

        public async Task<List<ProfileDTO>> getAllProfiles()
        {
            List<ProfileDTO> result = new List<ProfileDTO>();

            try
            {
                List<Profile> profiles = await _dataContext.Profiles.ToListAsync();

                foreach(Profile profile in profiles) 
                {
                    result.Add(new ProfileDTO()
                    {
                        id = profile.Id,
                        adress = profile.Adress,
                        bio = profile.Bio,
                        name = profile.Name,
                        userName = profile.UserName
                    });                             
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                throw;
            }
            return result;

        }
    }
}
