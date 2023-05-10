using MongoDB.Bson;

namespace KweetReadService.Models.MongoDb
{
    public abstract class Document : IDocument
    {
        //public ObjectId Id { get; set; }

        public string Id { get; set; }
        
    }
}
