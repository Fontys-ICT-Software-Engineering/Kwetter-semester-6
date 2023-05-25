using AutoMapper;
using KweetReadService.Data.Likes;
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
                ReactionKweetModel reaction = new ReactionKweetModel
                {
                    Id = dto.Id.ToString(),
                    KweetId = dto.KweetId,
                    UserId = dto.UserId,
                    Message = dto.Message,
                    DateSend = dto.Created
                };

                await _reactionsRepository.InsertOneAsync(reaction);
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool DeleteReactionKweet(Guid Id)
        {
            try
            {
                _reactionsRepository.DeleteByIdAsync(Id.ToString());
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public Task GDPRDelete(List<string> ids, string userId)
        {
            return Task.Run(() =>
            {
                foreach (string id in ids)
                {
                    _reactionsRepository.DeleteByKweetId(x => x.Id == id);
                }

                _reactionsRepository.DeleteManyByUserID(x => x.UserId == userId);
            });
        }

        public List<GetReactionDTO> GetReactionsByTweet(string kweetId)
        {
            List<GetReactionDTO> Dtos = new();

            try
            {
                //hier where voor performance, met where laad hij niet de hele dataset in?
                IEnumerable<ReactionKweetModel> kweets = _reactionsRepository.FilterBy(x => x.KweetId == kweetId);

                foreach (ReactionKweetModel reactionKweet in kweets)
                {
                    Dtos.Add(
                        new GetReactionDTO
                        {
                            Id = reactionKweet.Id,
                            KweetId = reactionKweet.KweetId,
                            UserId = reactionKweet.UserId,
                            Message = reactionKweet.Message,
                            DateSend = reactionKweet.DateSend
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

        public Task<bool> UpdateReactionKweet()
        {
            throw new NotImplementedException();
        }
    }
}
