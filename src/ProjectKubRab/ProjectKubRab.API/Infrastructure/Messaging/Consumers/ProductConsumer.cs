using DnsClient.Internal;
using ProjectKubRab.API.Core.Entities;
using ProjectKubRab.API.Core.Interfaces.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ProjectKubRab.API.Infrastructure.Messaging.Consumers
{
    public class ProductConsumer : BackgroundService
    {
        private readonly IProductService _productService;
        private readonly IConnectionFactory _factory;
        private readonly ILogger<ProductConsumer> _logger;

        public ProductConsumer(IProductService productService, IConnectionFactory factory, ILogger<ProductConsumer> logger)
        {
            _productService = productService;
            _factory = factory;
            _logger = logger;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("ProductConsumer is starting.");
            await using var connection = await _factory.CreateConnectionAsync(stoppingToken);
            await using var channel = await connection.CreateChannelAsync(options: null, stoppingToken);

            await channel.QueueDeclareAsync(queue: "product",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var consumer = new AsyncEventingBasicConsumer(channel);
            _logger.LogInformation("ProductConsumer is ready to receive messages in 'product' queue.");
            consumer.ReceivedAsync += async (sender, eventArgs) =>
            {
                _logger.LogInformation("Product queue message received.");
                try
                {
                    var body = eventArgs.Body.ToArray();
                    var message = System.Text.Encoding.UTF8.GetString(body);

                    var product = System.Text.Json.JsonSerializer.Deserialize<Core.Models.ViewModels.ProductViewModel>(message);
                    if (product is object)
                    {
                        _logger.LogInformation("Product {product} received.", product.Name);
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
