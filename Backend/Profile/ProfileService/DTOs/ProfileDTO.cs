namespace ProfileService.DTOs
{
    public class ProfileDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;

        public string Adress { get; set; } = string.Empty;

        public string Bio { get; set; } = string.Empty;
    }
}
