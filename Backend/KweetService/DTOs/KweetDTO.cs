using System.ComponentModel.DataAnnotations;

namespace Kweet.DTOs
{
    public class KweetDTO
    {
        public int Id { get; set; }

        public string Message { get; set; }

        public string User { get; set; }

        public DateTime Date { get; set; }


        public KweetDTO() { }

        public KweetDTO(int id, string message, string user, DateTime time)
        {
            Id = id;
            Message = message;
            User = user;
            //veranderen naar datum gecreeerd
            Date = time;
        }


    }
}
