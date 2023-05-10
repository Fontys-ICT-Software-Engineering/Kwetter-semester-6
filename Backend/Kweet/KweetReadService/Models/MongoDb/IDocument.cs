using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace KweetReadService.Models.MongoDb
{
    public interface IDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        string Id { get; set; }

        //DateTime CreatedAt { get; }
    }
}
