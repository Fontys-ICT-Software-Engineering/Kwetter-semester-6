using Kweet.DTOs;

namespace Kweet.Services.Kweet
{
    public interface IKweetService
    {
        public Task<List<KweetDTO>> getAllKweets();

        public Task<KweetDTO> postKweet(KweetDTO kweetDTO);

        public Task<bool> deleteKweet(int id);

        public Task<KweetDTO> getKweetById(int id);


    }
}
