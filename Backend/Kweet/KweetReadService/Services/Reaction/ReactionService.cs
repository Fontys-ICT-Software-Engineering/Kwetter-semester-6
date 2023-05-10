using AutoMapper;
using KweetReadService.Data.Reaction;
using KweetReadService.DTOs.ReactionDTO;
using KweetReadService.Models;
using MassTransit;
using SharedClasses.Reaction;

namespace KweetReadService.Services.Reaction
{
    public class ReactionService : IReactionService
    {
        private readonly IReactionMongoRepository<ReactionKweetModel> _reactionsRepository;
        private readonly IMapper _mapper;

        public ReactionService(IReactionMongoRepository<ReactionKweetModel> context, IMapper mapper)
        {
            _reactionsRepository = context;
            _mapper = mapper;
        }

        public async Task<bool> CreateReactionKweet(WriteCreateReactionKweet dto)
        {
            try
            {
                ReactionKweetModel reaction = new ReactionKweetModel(dto.KweetId, dto.UserId, dto.Message);

                await _reactionsRepository.InsertOneAsync(reaction);
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<GetReactionDTO> GetReactionsByTweet(string kweetId)
        {
            List<GetReactionDTO> Dtos = new();

            try
            {
                //hier where voor performance, met where laad hij niet de hele dataset in?
                IEnumerable<ReactionKweetModel> kweets = _reactionsRepository.FilterBy(x => x.KweetId == kweetId);

                foreach (ReactionKweetModel kweet in kweets)
                {
                    Dtos.Add(
                        new GetReactionDTO
                        {
                            Id = kweet.Id,
                            KweetId = kweet.KweetId,
                            UserId = kweet.UserId,
                            Message = kweet.Message,
                            DateSend = kweet.DateSend
                        }
                    );
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
