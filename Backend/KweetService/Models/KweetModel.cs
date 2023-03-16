using System.ComponentModel.DataAnnotations;

namespace Kweet.Models
{
    public class KweetModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        public string User { get; set; }

        [Required]
        public DateTime Date { get; set; }   


        public KweetModel(string message, string user, DateTime date)
        {
            Message = message;
            User = user;
            Date = date;
        }

    }
}
