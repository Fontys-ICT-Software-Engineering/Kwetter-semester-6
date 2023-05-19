namespace SharedClasses.Reaction
{
    public class WriteCreateReactionKweet
    {
        public Guid Id { get; set; }

        public string KweetId { get; set; } = string.Empty;

        public string UserId { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;

        public DateTime Created { get; set; }

    }
}
