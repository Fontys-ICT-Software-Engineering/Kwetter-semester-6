using AutoMapper;
using Kweet.Data;
using KweetService.DTOs.ReactionDTO;
using KweetService.Models;
using Microsoft.EntityFrameworkCore;

namespace KweetService.Services.Reaction
{
    public class ReactionService : IReactionService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public ReactionService(DataContext context, IMapper mapper)
        {
            _dataContext = context;
            _mapper = mapper;
        }





        public async Task<PostReactionKweetDTO> ReactionKweet(PostReactionKweetDTO dto)
        {
            try
            {
                ReactionKweet reaction = new ReactionKweet(dto.KweetId, dto.UserId, dto.Message);
                _dataContext.Reactions.Add(reaction);
                await _dataContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {

                throw;
            }

            return dto;
        }

        public List<GetReactionDTO> GetReactionsByTweet(string kweetId)
        {
            List<GetReactionDTO> Dtos = new();

            try
            {
 
                var kweets = _dataContext.Reactions.Where(i => i.KweetId == kweetId);

                foreach(ReactionKweet reaction in kweets) 
                { 
                    Dtos.Add(_mapper.Map<GetReactionDTO>(reaction));                                          
                }
                return Dtos;
            }
            catch (Exception ex)
            {
                return Dtos;
            }         
        }
    }
}
