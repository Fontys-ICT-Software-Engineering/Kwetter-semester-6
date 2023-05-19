using System.ComponentModel.DataAnnotations;

namespace KweetWriteService.DTOs.KweetDTO
{
    public class PostKweetDTO
    {
        public string Message { get; set; } = string.Empty;

        public string User { get; set; } = string.Empty;

    }
}
