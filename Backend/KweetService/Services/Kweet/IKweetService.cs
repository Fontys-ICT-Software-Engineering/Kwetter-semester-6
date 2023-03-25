using KweetService.DTOs;
using KweetService.DTOs.KweetDTO;
using KweetService.Models;

namespace Kweet.Services.Kweet
{
    public interface IKweetService
    {
        public Task<List<KweetDTO>> getAllKweets();

        public Task<PostKweetDTO> postKweet(PostKweetDTO postKweetDTO);

        public Task<bool> deleteKweet(Guid id);

        public Task<KweetDTO> getKweetById(Guid id);

        public Task<LikeKweetDTO> LikeKweet(LikeKweetDTO like);

        public Task<ReactionKweetDTO> ReactionKweet(ReactionKweetDTO like);

    }
}
