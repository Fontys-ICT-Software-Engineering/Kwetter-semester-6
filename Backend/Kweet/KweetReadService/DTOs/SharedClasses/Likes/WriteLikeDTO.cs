using KweetReadService.DTOs.SharedClasses;

namespace SharedClasses.Likes
{
    public class WriteLikeDTO
    {

        public Guid Id { get; set; }
        public string KweetId { get; set; } 

        public string UserId { get; set; }

        public LikeStatus status { get; set; } 

    }
}
