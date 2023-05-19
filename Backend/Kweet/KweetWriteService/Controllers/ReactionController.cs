using KweetWriteService.DTOs.KweetDTO;
using KweetWriteService.DTOs.LikeDTO;
using KweetWriteService.DTOs.ReactionDTO;
using KweetWriteService.Services.Likes;
using KweetWriteService.Services.Reaction;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KweetWriteService.Controllers
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
                List<GetReactionDTO> response = await _reactionservice.GetReactionsByTweet(kweetID);
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
                res = await _reactionservice.CreateReactionKweet(dto);
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
            try
            {
                bool succes = await _reactionservice.DeleteReactionKweet(Guid.Parse(reactionKweetID));
                if (succes) return Ok(succes);
                return BadRequest(succes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private static string GetUserID(ClaimsIdentity identity)
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
