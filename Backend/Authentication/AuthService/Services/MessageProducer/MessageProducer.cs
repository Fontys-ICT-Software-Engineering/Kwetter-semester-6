using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace AuthService.Services.MessageProducer
{
    public class MessageProducer : IMessageProducer
    {

        public MessageProducer() { }


        public void SendingMessage<T>(T message)
        {
            IConnectionFactory factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "myuser",
                Password = "mypassword",
                VirtualHost = "/"
            };

            var conn = factory.CreateConnection();

            using var channel = conn.CreateModel();

                //channel.ExchangeDeclare("test", ExchangeType.Topic, true);

                channel.QueueDeclare("Profile", durable: true, exclusive: true);
                
                //channel.QueueBind("Profile", ExchangeType.Topic, "Profile")

                var jsonString = JsonSerializer.Serialize(message);
                var body = Encoding.UTF8.GetBytes(jsonString);

                channel.BasicPublish("", "Profile", body: body);
            
        }
    }
}
