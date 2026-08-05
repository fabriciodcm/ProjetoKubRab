
namespace ProjectKubRab.Test.Application.Models
{
    public static class ProductHtmlContentMock
    {
        public static string Create()
        {
            var content = new System.Text.StringBuilder();

            content.Append("<html lang='pt-BR'>");
            content.Append("<head>");
            content.Append("<meta charset='UTF-8'>");
            content.Append("<meta name='viewport' content='width=device-width, initial-scale=1.0'>");
            content.Append("<title>Produtos - GPUs</title>");
            content.Append("</head>");
            content.Append("<body>");
            content.Append("<main class='container'>");
            content.Append("<h1>Lista de Produtos</h1>");
            content.Append("<ul id='products-list'>");
            content.Append("<li>");
            content.Append("<span class='name'>MSI RTX 5060 Ti Ventus 2X OC Plus</span>");
            content.Append("<span class='price'>R$&nbsp;2.853,78</span>");
            content.Append("</li>");
            content.Append("<li>");
            content.Append("<span class='name'>MSI GeForce RTX 5070 12G VENTUS 2X OC</span>");
            content.Append("<span class='price'>R$&nbsp;4.450,16</span>");
            content.Append("</li>");
            content.Append("<li>");
            content.Append("<span class='name'>ASRock RX 9060 XT CL 16GB AMD Radeon</span>");
            content.Append("<span class='price'>R$&nbsp;2.969,77</span>");
            content.Append("</li>");
            content.Append("<li>");
            content.Append("<span class='name'>ASRock RX 9070 XT Challenger AMD 16GB</span>");
            content.Append("<span class='price'>R$&nbsp;4.237,38</span>");
            content.Append("</li>");
            content.Append("<li>");
            content.Append("<span class='name'>Asus TUF-RTX 5070 TI 16G GAMING 16GB</span>");
            content.Append("<span class='price'>R$&nbsp;8.986,65</span>");
            content.Append("</li>");
            content.Append("</ul>");
            content.Append("</main>");
            content.Append("</body>");
            content.Append("</html>");
            return content.ToString();
        }
    }
}
