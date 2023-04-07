using KweetService.DTOs.KweetDTO;
using KweetService.DTOs.LikeDTO;
using KweetService.DTOs.ReactionDTO;
using KweetService.Models;

namespace Kweet.Services.Kweet
{
    public interface IKweetService
    {
        public Task<List<ReturnKweetDTO>> getAllKweets();

        public Task<PostKweetDTO> postKweet(PostKweetDTO postKweetDTO);

        public Task<bool> deleteKweet(Guid id);

        public Task<ReturnKweetDTO> getKweetById(Guid id);

        public Task<ReturnUpdateKweetDTO> UpdateKweet(PostUpdateKweetDTO dto);
    }
}
