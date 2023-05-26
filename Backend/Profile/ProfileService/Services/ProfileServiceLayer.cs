using AuthService.DTOs;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using ProfileService.Data;
using ProfileService.DTOs;
using ProfileService.Models;
using System.Linq;

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

                if (_dataContext.Profiles.Any(u => u.AuthId == dto.AuthId)) throw new Exception("User already exists");

                _dataContext.Profiles.Add(profile);
                await _dataContext.SaveChangesAsync();
            }
            catch (Exception)
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
                        Id = profile.Id,
                        Adress = profile.Adress,
                        Bio = profile.Bio,
                        Name = profile.Name,
                        UserName = profile.UserName
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

        public async Task GDPRDelete(string Id)
        {
           await _dataContext.Profiles.Where(x => x.AuthId == Guid.Parse(Id)).ExecuteDeleteAsync();
        }
    }
}
