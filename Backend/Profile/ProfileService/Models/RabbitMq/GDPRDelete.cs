namespace Sharedclasses.RabbitMq
{
    public class GDPRDelete
    {
        public string Id { get; set; } = string.Empty;

        public GDPRDelete(string id) 
        { 
            Id = id;         
        }


    }
}
