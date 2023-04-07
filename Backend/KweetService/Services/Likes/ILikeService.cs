using KweetService.DTOs.LikeDTO;

namespace KweetService.Services.Likes
{
    public interface ILikeService
    {
        public Task<bool> LikeKweet(PostLikeKweetDTO dto);

        //public Task<int> GetTotalLikesByTweet(string tweetId);

    }
}
