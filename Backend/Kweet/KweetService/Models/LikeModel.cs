using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KweetService.Models
{
    public class LikeModel
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("Kweet")]
        public string KweetID { get; set; }
        [Required]
        public string UserID { get; set; }  

    }
}
