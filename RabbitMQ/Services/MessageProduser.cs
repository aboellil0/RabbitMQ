using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace RabbitMQ.Services
{
    public class MessageProduser : IMessageProduser
    {
        public async Task SendingMessages<T>(T message) 
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "user",
                Password = "password",
                VirtualHost = "/"
            };

            var conn = await factory.CreateConnectionAsync();
            using var channel = await conn.CreateChannelAsync();


            await channel.QueueDeclareAsync("bookings",durable: true, exclusive:true);

            var jsonString = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(jsonString);


            await channel.BasicPublishAsync("","bookings",body);

        }
    }
}
