using KweetReadService.DTOs.KweetDTO;
using SharedClasses.Kweet;

namespace KweetReadService.Services.Kweet
{
    public interface IKweetReadService
    {
        public Task<List<ReturnKweetDTO>> GetAllKweets(string userId);

        public Task<bool> PostKweet(WriteKweetDTO postKweetDTO);

        public Task<bool> DeleteKweet(WriteDeleteKweetDTO Dto);

        public Task<ReturnKweetDTO> GetKweetById(Guid id);

        public Task<bool> UpdateKweet(WriteKweetUpdateDTO dto);

        public Task GDPRDelete(string Id);

    }
}
