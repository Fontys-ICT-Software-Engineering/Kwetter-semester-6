using KweetService.DTOs.KweetDTO;
using KweetService.DTOs.LikeDTO;
using KweetService.DTOs.ReactionDTO;
using KweetService.Services.Likes;
using KweetService.Services.Reaction;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KweetService.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class ReactionController : Controller
    {

        private readonly IReactionService _reactionservice;

        public ReactionController(IReactionService reactionService)
        {
            _reactionservice = reactionService;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetReactionDTO>>> GetAllReactionKweets(string kweetID)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            try
            {
                List<GetReactionDTO> response = _reactionservice.GetReactionsByTweet(kweetID);
                if (response == null) return NotFound();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
            
        [HttpPost(Name = "ReactionKweet")]
        public async Task<ActionResult<bool>> ReactionKweet(PostReactionKweetDTO dto)
        {
            bool res = false;

            try
            {
                res = await _reactionservice.ReactionKweet(dto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete(Name = "DeleteReactionKweet")]
        public async Task<ActionResult<bool>> DeleteReactionKweet(string reactionKweetID)
        {
            return false;
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
