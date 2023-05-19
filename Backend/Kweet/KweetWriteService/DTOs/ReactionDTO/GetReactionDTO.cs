namespace KweetWriteService.DTOs.ReactionDTO
{
    public class GetReactionDTO
    {
        public Guid Id { get; set; }

        public string KweetId { get; set; } = string.Empty;

        public string UserId { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;     

        public DateTime DateSend { get; set; }  
    }
}
