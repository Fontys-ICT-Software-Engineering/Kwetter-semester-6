﻿using Kweet.Services.Kweet;
using KweetService.DTOs.KweetDTO;
using KweetService.DTOs.LikeDTO;
using KweetService.DTOs.ReactionDTO;
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

        [HttpGet("/kubernetes")]
        public string kubernetes()
        {
            return "this works!";
        }


        [HttpGet(Name = "GetAllKweets")]
        public async Task<ActionResult<List<ReturnKweetDTO>>> getAllKweets()
        {
            try
            {
                List<ReturnKweetDTO> response = await _kweetService.getAllKweets();
                if (response == null) return NotFound();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost(Name = "PostKweet")]
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
        public async Task<ActionResult<ReturnKweetDTO>> findById(Guid id)
        {
            try
            {
                ReturnKweetDTO response = await _kweetService.getKweetById(id);
                if (response == null) return NotFound();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut]
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

    }
}
