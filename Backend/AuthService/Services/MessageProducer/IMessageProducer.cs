namespace AuthService.Services.MessageProducer
{
    public interface IMessageProducer
    {
        public void SendingMessage<T>(T message);
    }
}
