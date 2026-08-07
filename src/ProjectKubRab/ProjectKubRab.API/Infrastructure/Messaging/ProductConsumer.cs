using ProjectKubRab.API.Core.Interfaces.Repositories;
using ProjectKubRab.API.Core.Interfaces.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ProjectKubRab.API.Infrastructure.Messaging
{
    public class ProductConsumer : BackgroundService
    {
        private readonly IProductService _productService;

        public ProductConsumer(IProductService productService)
        {
            _productService = productService;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var factory = new ConnectionFactory
            {
                HostName = "host.docker.internal",
                Port = 5672,
                UserName = "app",
                Password = "app123",
                AutomaticRecoveryEnabled = true
            };
            await using var connection = await factory.CreateConnectionAsync(stoppingToken);
            await using var channel = await connection.CreateChannelAsync(options: null, stoppingToken);

            await channel.QueueDeclareAsync(queue: "product",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (sender, eventArgs) =>
            {
                try
                {
                    var body = eventArgs.Body.ToArray();
                    var message = System.Text.Encoding.UTF8.GetString(body);

                    var product = System.Text.Json.JsonSerializer.Deserialize<Core.Models.ViewModels.ProductViewModel>(message);

                    if (product is object)
                    {
                        await _productService.RegisterReadingAsync(product);
                    }

                    await ((AsyncEventingBasicConsumer)sender).Channel.BasicAckAsync(eventArgs.DeliveryTag, multiple: false);
                }
                catch (Exception)
                {
                    // Recoloca a mensagem na fila em caso de falha.
                    if (channel.IsOpen)
                    {
                        await channel.BasicNackAsync(
                            eventArgs.DeliveryTag,
                            multiple: false,
                            requeue: true);
                    }

                    // Registre a exceção antes de relançar, se houver ILogger.
                    throw;
                }

            };

            var consumerTag = await channel.BasicConsumeAsync("product",
                                 autoAck: false,
                                 consumer: consumer,
                                 consumerTag: string.Empty,
                                 noLocal: false,
                                 exclusive: false,
                                 arguments: null,
                                 cancellationToken: stoppingToken);

            try
            {
                // Impede ExecuteAsync de terminar e descartar canal/conexão.
                await Task.Delay(Timeout.Infinite, stoppingToken);
            }
            catch (OperationCanceledException)
                when (stoppingToken.IsCancellationRequested)
            {
                // Encerramento normal do BackgroundService.
            }
            finally
            {
                if (channel.IsOpen)
                {
                    await channel.BasicCancelAsync(
                        consumerTag,
                        cancellationToken : CancellationToken.None);
                }
            }
        }
    }
}
