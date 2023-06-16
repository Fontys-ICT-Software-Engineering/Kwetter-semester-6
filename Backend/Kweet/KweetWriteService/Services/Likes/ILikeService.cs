using KweetWriteService.DTOs.LikeDTO;
using KweetWriteService.Models;

namespace KweetWriteService.Services.Likes
{
    public interface ILikeService
    {
        public Task<bool> LikeKweet(PostLikeKweetDTO dto);

        //public Task<int> GetTotalLikesByTweet(string tweetId);
        public List<LikeModel> GetLikesPerKweet();

        public void DeleteLike(Guid id);
    }
}
