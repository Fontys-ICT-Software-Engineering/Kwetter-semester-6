using KweetReadService.Data.MongoDB;
using KweetReadService.DTOs.KweetDTO;
using KweetReadService.DTOs.ReactionDTO;
using KweetReadService.Models;
using KweetReadService.Services.Kweet;
using KweetReadService.Services.Reaction;
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
        private readonly IReactionService _reactionService;

        public KweetReadController(IKweetReadService kweetReadService, IReactionService reactionService)
        {
            _kweetReadService = kweetReadService;
            _reactionService = reactionService;
        }

        [HttpGet("AllKweets")]
        [Authorize]
        public async Task<ActionResult<List<ReturnKweetDTO>>> GetAllKweetsByUserID()
        {
            //var identity = HttpContext.User.Identity as ClaimsIdentity;
            //test
            try
            {
                List<ReturnKweetDTO> res = await _kweetReadService.GetAllKweets("string");
                if (res == null) return NotFound("no kweets found");
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);    
            }
        }

        [HttpGet("ReactionsByKweet")]
        [Authorize]
        public ActionResult<List<GetReactionDTO>> GetAllReactionsByKweet(string kweetId)
        {
            try
            {
                List<GetReactionDTO> response = _reactionService.GetReactionsByTweet(kweetId);
                if (response == null) return NotFound("no Reactions Found");
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }



        }

        private string getUserID(ClaimsIdentity identity)
        {
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