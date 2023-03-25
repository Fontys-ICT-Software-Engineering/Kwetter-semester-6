using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KweetService.Models
{
    public class ReactionKweet
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("Kweet")]
        public string KweetId { get; set; }

        [Required]
        public string UserId { get; set; }
        [Required]
        public string Message { get; set; }

        public DateTime DateSend { get; set; }  

        public ReactionKweet() { }  


        public ReactionKweet(string kweetId, string userId, string message)
        {
            Id = Guid.NewGuid();
            KweetId = kweetId;
            UserId = userId;
            Message = message;
            DateSend = DateTime.Now;    
        }




    }
}
