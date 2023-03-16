using Kweet.DTOs;
using Kweet.Services.Kweet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kweet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KweetController : ControllerBase
    {
        private readonly IKweetService _kweetService;

        public KweetController(IKweetService kweetService)
        {
            _kweetService = kweetService;
        }

        [HttpGet]
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

        [HttpGet("/test")]
        public async Task<ActionResult<string>> test()
        {
            return "dit werkt wel";
        }

        [HttpPost]
        public async Task<ActionResult<KweetDTO>> postKweet(KweetDTO dto)
        {
            KweetDTO response = new KweetDTO(); 

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

        [HttpDelete]
        public async Task<ActionResult<string>> deleteKweet(int id)
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

        [HttpGet("/id")]
        public async Task<ActionResult<KweetDTO>> findById(int id)
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


        [HttpGet("/authenticate")]
        [Authorize(Policy = "admins")]
        public IActionResult Authenticate()
        {
            return Ok();
        }


    }
}
