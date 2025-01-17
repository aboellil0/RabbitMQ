using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace RabbitMQ.Services
{
    public class MessageProduser : IMessageProduser
    {
        public async Task SendingMessages<T>(T message) 
        {
            var factory = new ConnectionFactory
            {
                HostName = "127.0.0.1",
                Port = 5672,
                UserName = "guest",
                Password = "guest",
                ClientProvidedName = "Rabbit Sender APP",
                RequestedConnectionTimeout = TimeSpan.FromSeconds(30),
            };

            using var conn = await factory.CreateConnectionAsync();
            using var channel = await conn.CreateChannelAsync();

            string exchangeName = "BookingExhange";
            string routingKey = "demo-routing-key";
            string queueName = "BookingQueue";

            await channel.ExchangeDeclareAsync(exchangeName, ExchangeType.Direct);
            await channel.QueueDeclareAsync(queueName, false, false, false, null);
            await channel.QueueBindAsync(queueName, exchangeName, routingKey, null);

            var jsonString = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(jsonString);


            await channel.BasicPublishAsync(exchangeName, routingKey, body);
           

        }
    }
}
