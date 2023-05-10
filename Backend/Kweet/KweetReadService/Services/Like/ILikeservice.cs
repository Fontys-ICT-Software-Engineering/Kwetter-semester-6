
using SharedClasses.Likes;

namespace KweetReadService.Services.Like
{
    public interface Ilikeservice
    {
        public Task<bool> LikeKweet(WriteLikeDTO dto);

    }
}
