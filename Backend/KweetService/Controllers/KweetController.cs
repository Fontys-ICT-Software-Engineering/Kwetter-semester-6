using Kweet.Services.Kweet;
using KweetService.DTOs;
using KweetService.DTOs.KweetDTO;
using KweetService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kweet.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class KweetController : ControllerBase
    {
        private readonly IKweetService _kweetService;

        public KweetController(IKweetService kweetService)
        {
            _kweetService = kweetService;
        }

        [HttpGet(Name = "GetAllKweets")]
        public async Task<ActionResult<List<KweetDTO>>> getAllKweets()
        {
            try
            {
                List<KweetDTO> response = await _kweetService.getAllKweets();
                if (response == null) return NotFound();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost(Name = "PostKeet")]
        public async Task<ActionResult<PostKweetDTO>> postKweet(PostKweetDTO dto)
        {
            PostKweetDTO response = new PostKweetDTO(); 

            try
            {
                response = await _kweetService.postKweet(dto);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(dto);

        }

        [HttpDelete(Name = "DeleteKweet")]
        public async Task<ActionResult<string>> deleteKweet(Guid id)
        {
            try
            {
                if (!_kweetService.deleteKweet(id).Result) return BadRequest();
                return Ok(id);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("/id", Name = "GetKweetById")]
        public async Task<ActionResult<KweetDTO>> findById(Guid id)
        {
            try
            {
                KweetDTO response = await _kweetService.getKweetById(id);
                if (response == null) return NotFound();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("/LikeKweet")]
        public async Task<ActionResult<string>> LikeKweet(LikeKweetDTO dto)
        {
            try
            {
                LikeKweetDTO response = await _kweetService.LikeKweet(dto);
                return Ok(response);

            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpPost("/ReactionKweet")]
        public async Task<ActionResult<string>> ReactOnKweet(ReactionKweetDTO dto)
        {
            try
            {
                ReactionKweetDTO response = await _kweetService.ReactionKweet(dto);
                return Ok(response);

            }
            catch (Exception ex)
            {

                throw;
            }

        }

    }
}
