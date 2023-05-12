using KweetWriteService.DTOs.KweetDTO;
using KweetWriteService.DTOs.LikeDTO;
using KweetWriteService.DTOs.ReactionDTO;
using KweetWriteService.Models;

namespace Kweet.Services.Kweet
{
    public interface IKweetService
    {
        public Task<List<ReturnKweetDTO>> getAllKweets(string id);

        public Task<PostKweetDTO> postKweet(PostKweetDTO postKweetDTO);

        public Task<bool> deleteKweet(Guid id);

        public Task<ReturnKweetDTO> getKweetById(Guid id);

        public Task<ReturnUpdateKweetDTO> UpdateKweet(PostUpdateKweetDTO dto);
    }
}
