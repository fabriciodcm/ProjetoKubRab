using ProjectKubRab.Worker.Application.Abstractions;

namespace ProjectKubRab.Worker;

public class Worker(
    ILogger<Worker> logger,
    string[] products,
    IProductClient productClient,
    IProductHtmlExtractor productHtmlExtractor,
    IProductProducer productProducer) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        #if DEBUG
        if (products == null || products.Length == 0){
            products = new[] { "ASRock RX 9060 XT CL 16GB AMD Radeon" };
        }
        #endif
        if(products != null && products.Length > 0)
        {
            var messages = new HashSet<string>();

            foreach (var product in products)
            {
                if (!string.IsNullOrEmpty(product))
                {
                    logger.LogInformation("Worker is starting for product: {product}", product);
                    var htmlContent = await productClient.GetHtmlContentFromProductsPageAsync(stoppingToken);
                    logger.LogInformation("Products from ProfuctPage completed : {success}", !string.IsNullOrEmpty(htmlContent) ? "Success" : "Failed");
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

                        var jsonContent = System.Text.Json.JsonSerializer.Serialize(extractedProduct);

                        messages.Add(jsonContent);
                    }
                }
                await productProducer.Execute(messages.Where(x => !String.IsNullOrEmpty(x)).ToArray(), stoppingToken);
                logger.LogInformation("Products added to queue : {products}", messages.Count);
            }
        }
        else
        {
            logger.LogWarning("No product specified. Worker will not perform any actions.");
            return;
        }
    }
}
