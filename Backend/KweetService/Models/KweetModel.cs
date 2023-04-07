using System.ComponentModel.DataAnnotations;

namespace Kweet.Models
{
    public class KweetModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        public string User { get; set; }

        [Required]
        public DateTime Date { get; set; }   

        public bool IsEdited { get; set; }

        public KweetModel(string message, string user)
        {
            Id = Guid.NewGuid();
            Message = message;
            User = user;
            Date = DateTime.Now;
            IsEdited = false;
        }

    }
}
