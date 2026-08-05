using HtmlAgilityPack;
using ProjectKubRab.Worker.Application.Abstractions;
using ProjectKubRab.Worker.Application.Models;
using System.Net;

namespace ProjectKubRab.Worker.Infrastructure.Parsing;

public sealed class ProductHtmlExtractor : IProductHtmlExtractor
{
    public async Task<ProductMatch?> ExtractDesiredProductAsync(string htmlContent, string desiredProduct)
    {
        //Implementacao utilizando HtmlAgilityPack para o código ficar mais legível 
        HtmlWeb web = new HtmlWeb();
        HtmlDocument doc = new HtmlDocument();
        doc.LoadHtml(htmlContent);
        HtmlNodeCollection productsList = doc.DocumentNode.SelectNodes("//ul[@id='products-list']/li");

        if (productsList is not null && productsList.Count > 0)
        {
            foreach (HtmlNode product in productsList)
            {
                var name = product.SelectSingleNode("span[@class='name']");
                var price = product.SelectSingleNode("span[@class='price']");
                if (name.InnerText == desiredProduct)
                {
                    return new ProductMatch(NormalizeText(name.InnerText), NormalizeText(price.InnerText));
                }
            }
        }
        return null;
    }
    private static string NormalizeText(string textFromHtml)
    {
        return WebUtility.HtmlDecode(textFromHtml).Replace("\u00A0", string.Empty).Trim();
    }
}
