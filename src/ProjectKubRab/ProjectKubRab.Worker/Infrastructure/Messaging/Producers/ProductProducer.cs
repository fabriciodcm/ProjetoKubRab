using ProjectKubRab.Worker.Application.Abstractions;
using RabbitMQ.Client;

namespace ProjectKubRab.Worker.Infrastructure.Messaging.Producers
{
    public class ProductProducer : IProductProducer
    {
        public async Task Execute(string[] messages, CancellationToken stoppingToken)
        {
            var factory = new ConnectionFactory
            {
                HostName = "rabbitmq",
                Port = 5672,
                UserName = "app",
                Password = "app123"
            };

            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue: "product",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            foreach (var message in messages)
            {
                var body = System.Text.Encoding.UTF8.GetBytes(message);

                await channel.BasicPublishAsync(exchange: string.Empty,
                                     routingKey: "product",
                                     body: body);
            }
        }
    }
}
