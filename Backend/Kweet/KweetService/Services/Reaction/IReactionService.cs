using KweetService.DTOs.ReactionDTO;

namespace KweetService.Services.Reaction
{
    public interface IReactionService
    {
        public Task<bool> ReactionKweet(PostReactionKweetDTO dto);

        public  List<GetReactionDTO> GetReactionsByTweet(string kweetId);

    }

}
