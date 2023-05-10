using KweetReadService.Models.MongoDb;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KweetReadService.Models
{
    [BsonCollection("Reactions")]
    public class ReactionKweetModel : Document
    {
        public Guid Id { get; set; }

        public string KweetId { get; set; }

        public string UserId { get; set; }

        public string Message { get; set; }

        public DateTime DateSend { get; set; }  

        public ReactionKweetModel() { }  

        public ReactionKweetModel(string kweetId, string userId, string message)
        {
            Id = Guid.NewGuid();
            KweetId = kweetId;
            UserId = userId;
            Message = message;
            DateSend = DateTime.Now;    
        }
    }
}
