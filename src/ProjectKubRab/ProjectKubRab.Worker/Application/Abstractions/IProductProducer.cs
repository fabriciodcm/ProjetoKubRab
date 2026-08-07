
namespace ProjectKubRab.Worker.Application.Abstractions
{
    public interface IProductProducer
    {
        Task Execute(string[] message, CancellationToken stoppingToken);
    }
}
