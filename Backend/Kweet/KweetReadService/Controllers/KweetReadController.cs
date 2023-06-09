using Amazon.Runtime.Internal.Util;
using KweetReadService.Data.MongoDB;
using KweetReadService.DTOs.KweetDTO;
using KweetReadService.DTOs.ReactionDTO;
using KweetReadService.Models;
using KweetReadService.Services.Kweet;
using KweetReadService.Services.Reaction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedClasses.Kweet;
using SharedClasses.Reaction;
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

        [HttpGet("k6s")]
        public ActionResult DummyData()
        {
            try
            {
                Guid kweetid1 = Guid.NewGuid();
                WriteKweetDTO kweet1 = new WriteKweetDTO {Id = kweetid1, Date = DateTime.Now, Message = "TestMessage for k6s load test 1", IsEdited = false, User = "string" };
                WriteKweetDTO kweet2 = new WriteKweetDTO { Id = Guid.NewGuid(), Date = DateTime.Now, Message = "TestMessage for k6s load test 2", IsEdited = false, User = "string" };
                WriteKweetDTO kweet3 = new WriteKweetDTO { Id = Guid.NewGuid(), Date = DateTime.Now, Message = "TestMessage for k6s load test 3", IsEdited = false, User = "string" };

                WriteCreateReactionKweet reaction1 = new WriteCreateReactionKweet { Id = Guid.NewGuid(), Created = DateTime.Now, KweetId = kweetid1.ToString(), Message = "reaction for k6s test load 1", UserId = "string" };
                WriteCreateReactionKweet reaction2 = new WriteCreateReactionKweet { Id = Guid.NewGuid(), Created = DateTime.Now, KweetId = kweetid1.ToString(), Message = "reaction for k6s test load 2", UserId = "string" };

                _kweetReadService.PostKweet(kweet1);
                _kweetReadService.PostKweet(kweet2);
                _kweetReadService.PostKweet(kweet3);
                _reactionService.CreateReactionKweet(reaction1);
                _reactionService.CreateReactionKweet(reaction2);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}