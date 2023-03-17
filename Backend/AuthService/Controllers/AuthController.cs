using AuthService.DTOs;
using AuthService.Services.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authService;

        public AuthController(IAuthenticationService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public async Task<ActionResult<string>> getAllUsers()
        {
            try
            {
                List<UserDTO> res = await _authService.GetAllUsers();
                if (res.Count == 0) return NotFound();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<CreateUserDTO>> createUser(CreateUserDTO dto)
        {
            try
            {
                CreateUserDTO res = await _authService.Register(dto);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}