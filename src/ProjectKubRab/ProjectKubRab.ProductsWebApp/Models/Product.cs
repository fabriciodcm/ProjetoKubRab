using ProjectKubRab.ProductsWebApp.Helpers;

namespace ProjectKubRab.ProductsWebApp.Models
{
    public class Product
    {
        private static readonly System.Globalization.CultureInfo BrazilianCulture =
            System.Globalization.CultureInfo.GetCultureInfo("pt-BR");

        public Product(string name, string price)
        {
            this.Name = name;
            this.Price = price;
        }

        public string Name { get; set; } 
        private string Price { get; set; }
        public string CalculatedPrice { get => this.CalculatePrice(this.Price); }

        private string CalculatePrice(string price)
        {
            var basePrice = decimal.Parse(
                price,
                System.Globalization.NumberStyles.Currency,
                BrazilianCulture);
            var calculatedPrice = PriceHelper.ApplyRandomPrice(basePrice, -0.07m, 0.07m);

            return calculatedPrice.ToString("C", BrazilianCulture);
        }
    }
}
