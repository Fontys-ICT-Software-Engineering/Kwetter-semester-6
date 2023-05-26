
using KweetReadService.Models;
using SharedClasses.Likes;

namespace KweetReadService.Services.Like
{
    public interface Ilikeservice
    {
        public Task<bool> LikeKweet(WriteLikeDTO dto);

        public Task<long> ReturnLikes(string kweetId);

        public Task<bool> IsLikedByUser(string kweetId, string UserId);

        public Task GDPRDelete(List<string> ids, string Id);

    }
}
