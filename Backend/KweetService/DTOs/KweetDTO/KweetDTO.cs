using System.ComponentModel.DataAnnotations;

namespace KweetService.DTOs.KweetDTO
{
    public class KweetDTO
    {
        public Guid Id { get; set; }

        public string Message { get; set; }

        public string User { get; set; }

        public DateTime Date { get; set; }


        public KweetDTO() { }

        public KweetDTO(Guid id, string message, string user, DateTime time)
        {
            Id = id;
            Message = message;
            User = user;
            //veranderen naar datum gecreeerd
            Date = time;
        }


    }
}
