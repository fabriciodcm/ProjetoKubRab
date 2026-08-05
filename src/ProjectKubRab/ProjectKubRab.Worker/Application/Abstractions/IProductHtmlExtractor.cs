using ProjectKubRab.Worker.Application.Models;

namespace ProjectKubRab.Worker.Application.Abstractions;

public interface IProductHtmlExtractor
{
    Task<ProductMatch?> ExtractDesiredProductAsync(string htmlContent, string desiredProduct);
}
