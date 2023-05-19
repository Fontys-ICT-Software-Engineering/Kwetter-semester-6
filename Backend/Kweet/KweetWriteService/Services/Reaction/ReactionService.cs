using AutoMapper;
using Kweet.Data;
using KweetWriteService.DTOs.ReactionDTO;
using KweetWriteService.Models;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using SharedClasses;
using SharedClasses.Reaction;
using static MassTransit.Logging.DiagnosticHeaders.Messaging;

namespace KweetWriteService.Services.Reaction
{
    public class ReactionService : IReactionService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        private readonly IRequestClient<WriteCreateReactionKweet> _createClient;
        private readonly IRequestClient<WriteDeleteReactionKWeet> _deleteClient;

        public ReactionService(DataContext context, IMapper mapper, IRequestClient<WriteCreateReactionKweet> createClient, IRequestClient<WriteDeleteReactionKWeet> deleteClient)
        {
            _dataContext = context;
            _mapper = mapper;
            _createClient = createClient;
            _deleteClient = deleteClient;
        }

        public async Task<bool> CreateReactionKweet(PostReactionKweetDTO dto)
        {
            try
            {
                ReactionKweetModel reaction = new ReactionKweetModel(dto.KweetId, dto.UserId, dto.Message);
                _dataContext.Reactions.Add(reaction);
                await _dataContext.SaveChangesAsync();

                WriteCreateReactionKweet rabbit = _mapper.Map<WriteCreateReactionKweet>(reaction);
                var status = await _createClient.GetResponse<MassTransitResponse>(rabbit);

                if(!status.Message.Succes)
                {
                    _dataContext.Remove(_dataContext.Reactions.Single(r => r.Id == reaction.Id));
                    throw new Exception("Failed to Synchronize databases");
                }

                return true;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<bool> DeleteReactionKweet(Guid Id)
        {
            try
            {
                ReactionKweetModel model = _dataContext.Reactions.Single(r => r.Id.Equals(Id));


                _dataContext.Remove(model);

                WriteDeleteReactionKWeet rabbit = new WriteDeleteReactionKWeet(Id);
                var status = await _deleteClient.GetResponse<MassTransitResponse>(rabbit);

                if(!status.Message.Succes)
                {
                    _dataContext.Add(model);
                    _dataContext.SaveChanges();
                    throw new Exception("Failed to synchronize databases");
                }
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<GetReactionDTO>> GetReactionsByTweet(string kweetId)
        {
            List<GetReactionDTO> Dtos = new();

            try
            {
                //hier where voor performance, met where laad hij niet de hele dataset in?
                var kweets = await _dataContext.Reactions.Where(i => i.KweetId == kweetId).ToListAsync();

                foreach(ReactionKweetModel reaction in kweets) 
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
