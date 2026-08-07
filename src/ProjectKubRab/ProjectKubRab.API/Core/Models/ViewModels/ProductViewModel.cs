using System.ComponentModel.DataAnnotations;

namespace ProjectKubRab.API.Core.Models.ViewModels
{
    public class ProductViewModel
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Price { get; set; } = string.Empty;

        public DateOnly? DateAdded { get; set; } 

        public static implicit operator ProductViewModel(Entities.Product product)
        {
            return new ProductViewModel
            {
                Name = product.Name,
                Price = product.Price.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("pt-BR")),
                DateAdded = product.DateAdded
            };
        }

        public static implicit operator Entities.Product(ProductViewModel viewModel)
        {
            return new Entities.Product
            {
                Name = viewModel.Name,
                Price = decimal.Parse(viewModel.Price,
                    System.Globalization.NumberStyles.Currency,
                    System.Globalization.CultureInfo.GetCultureInfo("pt-BR")
                )
            };
        }
    }
}
