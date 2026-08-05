using ProjectKubRab.Worker.Application.Abstractions;

namespace ProjectKubRab.Worker;

public class Worker(
    ILogger<Worker> logger,
    string product,
    IProductClient productClient,
    IProductHtmlExtractor productHtmlExtractor) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        #if DEBUG
        product = String.IsNullOrEmpty(product) ? "ASRock RX 9060 XT CL 16GB AMD Radeon" : product;
        #endif

        if (!string.IsNullOrEmpty(product))
        {
            logger.LogInformation("Worker is starting for product: {product}", product);
            var htmlContent = await productClient.GetHtmlContentFromProductsPageAsync(stoppingToken);
            var extractedProduct = await productHtmlExtractor.ExtractDesiredProductAsync(htmlContent, product);

            if (extractedProduct is null)
            {
                logger.LogWarning("Product not found in HTML content. Desired product: {product}", product);
                return;
            }
            else
            {
                logger.LogInformation(
                    "Product found in HTML content. Product: {product}. Price: {price}",
                    extractedProduct.Name,
                    extractedProduct.Price);

                //TODO inserir no RabbitMQ
                await Task.Delay(1000, stoppingToken);
            }
        }
        else
        {
            logger.LogWarning("No product specified. Worker will not perform any actions.");
            return;
        }
    }
}
