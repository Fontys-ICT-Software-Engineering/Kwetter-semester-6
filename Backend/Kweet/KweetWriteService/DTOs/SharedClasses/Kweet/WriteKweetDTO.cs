namespace SharedClasses.Kweet
{
    public class WriteKweetDTO
    {
        public Guid Id { get; set; }

        public string Message { get; set; }

        public string User { get; set; }

        public DateTime Date { get; set; }

        public bool IsEdited { get; set; }
    }
}
