using KweetService.DTOs.ReactionDTO;

namespace KweetService.Services.Reaction
{
    public interface IReactionService
    {
        public Task<PostReactionKweetDTO> ReactionKweet(PostReactionKweetDTO dto);

        public List<GetReactionDTO> GetReactionsByTweet(string kweetId);

    }

}
