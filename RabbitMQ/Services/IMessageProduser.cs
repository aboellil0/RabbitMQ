namespace RabbitMQ.Services
{
    public interface IMessageProduser
    {
        public void SendingMessages<T>(T message);
    }
}
