namespace SharedClasses.Kweet
{
    public class WriteKweetDTO
    {
        public Guid Id { get; set; }

        public string Message { get; set; } = string.Empty;

        public string User { get; set; } = string.Empty;

        public DateTime Date { get; set; }

        public bool IsEdited { get; set; }
    }
}
