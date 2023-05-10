namespace KweetReadService.DTOs.KweetDTO
{
    public class ReturnUpdateKweetDTO
    {
        public Guid Id { get; set; }

        public string User { get; set; }  

        public string Message { get; set; }

        public bool IsEdited { get; set; }

    }
}
