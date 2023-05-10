using KweetReadService.DTOs.ReactionDTO;
using SharedClasses.Reaction;

namespace KweetReadService.Services.Reaction
{
    public interface IReactionService
    {
        public Task<bool> CreateReactionKweet(WriteCreateReactionKweet dto);

        public List<GetReactionDTO> GetReactionsByTweet(string kweetId);

    }
}
