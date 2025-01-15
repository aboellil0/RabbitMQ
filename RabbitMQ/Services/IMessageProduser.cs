namespace RabbitMQ.Services
{
    public interface IMessageProduser
    {
        public Task SendingMessages<T>(T message);
    }
}
