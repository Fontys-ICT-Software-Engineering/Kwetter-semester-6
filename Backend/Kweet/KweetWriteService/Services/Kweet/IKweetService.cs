using KweetWriteService.DTOs.KweetDTO;
using KweetWriteService.DTOs.LikeDTO;
using KweetWriteService.DTOs.ReactionDTO;
using KweetWriteService.Models;

namespace Kweet.Services.Kweet
{
    public interface IKweetService
    {
        public Task<List<ReturnKweetDTO>> GetAllKweets(string id);

        public Task<PostKweetDTO> PostKweet(PostKweetDTO postKweetDTO);

        public Task<bool> DeleteKweet(Guid id);

        public Task<ReturnKweetDTO> GetKweetById(Guid id);

        public Task<ReturnUpdateKweetDTO> UpdateKweet(PostUpdateKweetDTO dto);

        public Task GDPRDelete(string Id);
    }
}
