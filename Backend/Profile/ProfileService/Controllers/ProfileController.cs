using Microsoft.AspNetCore.Mvc;
using ProfileService.DTOs;
using ProfileService.Services;
using System.Security.Claims;

namespace ProfileService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpGet("/[controller]/kubernetes")]
        public string kubernetes()
        {
            return "this works!";
        }

        [HttpGet("/[Controller]")]
        public async Task<ActionResult<List<ProfileDTO>>> getAllProfiles()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            try
            {
                List<ProfileDTO> response = await _profileService.getAllProfiles()
;                if (response == null) return NotFound();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/[Controller]/test")]
        public async Task<ActionResult<List<ProfileDTO>>> test(string Id)
        {
            await _profileService.GDPRDelete(Id);
            return Ok();
        }
    }
}