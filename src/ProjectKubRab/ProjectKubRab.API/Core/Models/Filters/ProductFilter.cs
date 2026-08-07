using System.ComponentModel.DataAnnotations;

namespace ProjectKubRab.API.Core.Models.Filters
{
    public class ProductFilter
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        public DateOnly? InitialDate { get; set; }
        public DateOnly? FinalDate { get; set; }
    }
}
