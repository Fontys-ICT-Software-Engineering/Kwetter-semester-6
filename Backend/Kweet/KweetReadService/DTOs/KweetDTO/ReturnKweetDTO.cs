using System.ComponentModel.DataAnnotations;

namespace KweetReadService.DTOs.KweetDTO
{
    public class ReturnKweetDTO
    {
        public string Id { get; set; }

        public string Message { get; set; }

        public string User { get; set; }

        public DateTime Date { get; set; }

        public bool IsEdited { get; set; }

        public int Likes { get; set; }

        public bool Liked { get; set; }

        public ReturnKweetDTO() { }

        public ReturnKweetDTO(string id, string message, string user, DateTime time, bool isEdited, int likes, bool liked)
        {
            Id = id.ToString();
            Message = message;
            User = user;
            Date = time;
            IsEdited = isEdited;
            Likes = likes;
            Liked = liked;
        }
    }
}
