using RabbitMQ.Client;

namespace RabbitMQ.Services
{
    public class MessageProduser : IMessageProduser
    {
        public void SendingMessages<T>(T message) 
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "user",
                Password = "password",

            };
        }
    }
}
