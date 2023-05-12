using KweetWriteService.DTOs.LikeDTO;

namespace KweetWriteService.Services.Likes
{
    public interface ILikeService
    {
        public Task<bool> LikeKweet(PostLikeKweetDTO dto);

        //public Task<int> GetTotalLikesByTweet(string tweetId);

    }
}
