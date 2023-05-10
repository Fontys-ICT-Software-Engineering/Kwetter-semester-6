using KweetReadService.Models.MongoDb;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KweetReadService.Models
{
    [BsonCollection("Likes")]
    public class LikeModel : Document
    {
        
        //public string Id { get; set; }
        
        public string KweetID { get; set; }
        
        public string UserID { get; set; }  

    }
}
