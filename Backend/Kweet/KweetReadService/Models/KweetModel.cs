using KweetReadService.Models.MongoDb;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace KweetReadService.Models
{
    [BsonCollection("Kweets")]
    public class KweetModel : Document
    {
        //public Guid Id { get; set; }

        public string Message { get; set; }

        public string User { get; set; }

        public DateTime Date { get; set; }

        public bool IsEdited { get; set; }

        public KweetModel()
        {

        }

        public KweetModel(string message, string user)
        {
            Id = Guid.NewGuid().ToString();
            Message = message;
            User = user;
            Date = DateTime.Now;
            IsEdited = false;
        }

        //voor testing
        public KweetModel(Guid id, string message, string user)
        {
            Id = id.ToString();
            Message = message;
            User = user;
            Date = DateTime.Now;
            IsEdited = false;
        }

    }
}
