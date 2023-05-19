using Kweet.Services.Kweet;
using KweetWriteService.DTOs.KweetDTO;
using KweetWriteService.DTOs.LikeDTO;
using KweetWriteService.DTOs.ReactionDTO;
using KweetWriteService.Models;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedClasses;
using SharedClasses.Kweet;
using System.Security.Claims;

namespace Kweet.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class KweetWriteController : ControllerBase
    {
        private readonly IKweetService _kweetService;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IRequestClient<WriteKweetDTO> _client;

        public KweetWriteController(IKweetService kweetService, IPublishEndpoint endpoint, IRequestClient<WriteKweetDTO> client)
        {
            _kweetService = kweetService;
            _publishEndpoint = endpoint;
            _client = client;
        }

        [HttpGet("/[controller]/kubernetes")]
        public string kubernetes()
        {
            return "this works!";
        }


        [HttpGet("/[controller]/all")]
        [Authorize]
        public async Task<ActionResult<List<ReturnKweetDTO>>> getAllKweets()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            try
            {
                List<ReturnKweetDTO> response = await _kweetService.GetAllKweets(GetUserID(identity));
                if (response == null) return NotFound();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("/[controller]/post")]
        public async Task<ActionResult<PostKweetDTO>> postKweet(PostKweetDTO dto)
        {
            PostKweetDTO response = new PostKweetDTO(); 

            try
            {
                response = await _kweetService.PostKweet(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(dto);

        }

        [HttpDelete("/[controller]/delete")]
        public ActionResult<string> DeleteKweet(Guid id)
        {
            try
            {
                if (!_kweetService.DeleteKweet(id).Result) return BadRequest();
                return Ok(id);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("/[controller]/id", Name = "GetKweetById")]
        public async Task<ActionResult<ReturnKweetDTO>> findById(Guid id)
        {
            try
            {
                ReturnKweetDTO response = await _kweetService.GetKweetById(id);
                if (response == null) return NotFound();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("/[controller]/update")]
        public async Task<ActionResult<ReturnUpdateKweetDTO>> UpdateKweet(PostUpdateKweetDTO dto)
        {
            ReturnUpdateKweetDTO response = new();
            try
            {
                response = await _kweetService.UpdateKweet(dto);
                
                if(response == null) return NotFound();
                return Ok(response);    
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("/[controller]/rabbitmq")]
        public async Task<ActionResult<PostKweetDTO>> PostRabbitMq(WriteKweetDTO dto)
        {
            try
            {
               
                var response = await _client.GetResponse<MassTransitResponse>(dto);

                return Ok(response);
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
