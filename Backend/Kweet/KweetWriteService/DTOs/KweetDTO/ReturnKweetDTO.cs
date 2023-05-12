using System.ComponentModel.DataAnnotations;

namespace KweetWriteService.DTOs.KweetDTO
{
    public class ReturnKweetDTO
    {
        public Guid Id { get; set; }

        public string Message { get; set; }

        public string User { get; set; }

        public DateTime Date { get; set; }

        public bool IsEdited { get; set; }

        public int Likes { get; set; }

        public bool Liked { get; set; }

        public ReturnKweetDTO() { }

        public ReturnKweetDTO(Guid id, string message, string user, DateTime time, bool isEdited, int likes, bool liked)
        {
            Id = id;
            Message = message;
            User = user;
            Date = time;
            IsEdited = isEdited;
            Likes = likes;
            Liked = liked;
        }
    }
}
