using KweetReadService.Data.MongoDB;
using KweetReadService.DTOs.KweetDTO;
using KweetReadService.Models;
using KweetReadService.Services.Kweet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedClasses.Kweet;
using System;
using System.Security.Claims;

namespace KweetReadService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KweetReadController : ControllerBase
    {
        private readonly IKweetReadService _kweetReadService;

        public KweetReadController(IKweetReadService kweetReadService)
        {
            _kweetReadService = kweetReadService;
        }

        [HttpPost("PostKweet")]
        public async Task PostKweet(PostKweetDTO dto)
        {
            //try
            //{
            //    await _kweetReadService.PostKweet(dto);
            //}
            //catch (Exception ex)
            //{

            //    throw;
            //}

        }

        [HttpGet("Kweets")]
        [Authorize]
        public async Task<ActionResult<List<ReturnKweetDTO>>> GetAllKweetsByUserID()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            try
            {
                List<ReturnKweetDTO> res = await _kweetReadService.GetAllKweets(getUserID(identity));
                if (res == null) return NotFound("no kweets found");
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);    
            }
        }

        private string getUserID(ClaimsIdentity identity)
        {
            string auth = "Authorization";
            string Id = string.Empty;

            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                try
                {
                    Id = claims.FirstOrDefault(x => x.Type == "ID").Value;
                }
                catch (Exception)
                {
                    throw new Exception("Token Not available");
                }
            }
            return Id;
        }
    }
}