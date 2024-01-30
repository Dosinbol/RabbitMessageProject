using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMessageProjectConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Notification System.");

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

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, eventArgs) =>
                    {
                        var body = eventArgs.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine($"A notification has been received {message}");
                    };
                    channel.BasicConsume("soldier", autoAck: true, consumer);
                    Console.ReadKey();
                }
            }
        }
    }
}
