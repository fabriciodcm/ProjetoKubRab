using ProjectKubRab.API.Core.Entities;
using ProjectKubRab.API.Core.Models.Filters;
using ProjectKubRab.API.Core.Models.ViewModels;

namespace ProjectKubRab.API.Core.Interfaces.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductViewModel>> GetAllByIntervalAsync(ProductFilter filter);
        Task<bool> RegisterReadingAsync(ProductViewModel product);
    }
}
