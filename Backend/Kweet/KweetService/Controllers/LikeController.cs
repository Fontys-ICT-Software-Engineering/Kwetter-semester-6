using Kweet.Services.Kweet;
using KweetService.DTOs.KweetDTO;
using KweetService.DTOs.LikeDTO;
using KweetService.Services.Likes;
using Microsoft.AspNetCore.Mvc;

namespace KweetService.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class LikeController : ControllerBase
    {
        private readonly ILikeService _likeService;

        public LikeController(ILikeService likeService)
        {
            _likeService = likeService;
        }

        [HttpGet]
        public string test()
        {
            return "this works";
        }

        [HttpPost(Name = "LikeKweet")]
        public async Task<ActionResult<bool>> LikeKweet(PostLikeKweetDTO dto)
        {
            bool res = false;

            try
            {
                res = await _likeService.LikeKweet(dto);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }






    }
}
