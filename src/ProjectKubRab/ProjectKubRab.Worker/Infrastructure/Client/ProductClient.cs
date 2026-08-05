
using ProjectKubRab.Worker.Application.Abstractions;

namespace ProjectKubRab.Worker.Infrastructure.Client
{
    public class ProductClient(HttpClient _httpClient) : IProductClient
    {
        //TODO Utilizar Headless Browser para renderizar o HTML da página de produtos, caso necessário.
        public async Task<string> GetHtmlContentFromProductsPageAsync(CancellationToken stoppingToken)
        {
            return await _httpClient.GetStringAsync("Home/Products", stoppingToken);
        }
    }
}
