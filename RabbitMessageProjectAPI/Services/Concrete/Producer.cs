using RabbitMessageProjectAPI.Services.Abscract;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace RabbitMessageProjectAPI.Services.Concrete
{
    public class Producer : IProducer
    {
        public void SendMessage<T>(T message)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "user",
                Password = "pass",
                VirtualHost = "/"
            };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare("soldier", durable: true, exclusive: false);
                    var jsonString = JsonSerializer.Serialize(message);
                    var body = Encoding.UTF8.GetBytes(jsonString);

                    channel.BasicPublish("","soldier", body : body);
                }
            }
        }
    }
}
