namespace KweetReadService.DTOs.ReactionDTO
{
    public class GetReactionDTO
    {
        public string Id { get; set; }

        public string KweetId { get; set; }

        public string UserId { get; set; }

        public string Message { get; set; } 

        public DateTime DateSend { get; set; }  
    }
}
