namespace SharedClasses.Kweet
{
    public class WriteKweetUpdateDTO
    {
        public Guid Id { get; set; }

        public string User { get; set; }

        public string Message { get; set; }

        public bool IsEdited { get; set; }
    }
}
