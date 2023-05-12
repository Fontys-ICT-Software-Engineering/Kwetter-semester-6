namespace KweetReadService.DTOs.ReactionDTO
{
    public class PostReactionKweetDTO
    {
        public Guid Id { get; set; }

        public string KweetId { get; set; }

        public string UserId { get; set; }

        public string Message { get; set; }

    }
}
