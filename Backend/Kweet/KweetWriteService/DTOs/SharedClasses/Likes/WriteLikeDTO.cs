namespace SharedClasses.Likes
{
    public class WriteLikeDTO
    {

        public Guid Id { get; set; }
        public string KweetId { get; set; } = string.Empty; 

        public string UserId { get; set; } = string.Empty;

        public LikeStatus Status { get; set; } 

    }
}
