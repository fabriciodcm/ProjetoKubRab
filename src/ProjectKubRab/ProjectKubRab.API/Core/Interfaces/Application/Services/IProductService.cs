using ProjectKubRab.API.Core.Entities;
using ProjectKubRab.API.Core.Models.Filters;
using ProjectKubRab.API.Core.Models.ViewModels;

namespace ProjectKubRab.API.Core.Interfaces.Application.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductViewModel>> GetAllByIntervalAsync(ProductFilter filter);
        Task<bool> RegisterReadingAsync(ProductViewModel product);
        Task<bool> UpdateAsync(ProductViewModel product);
        Task<bool> InsertAsync(ProductViewModel product);
        Task<bool> DeleteAsync(string id);
        Task<ProductViewModel> GetByIdAsync(string id);
        Task<IEnumerable<ProductViewModel>> GetAllAsync();
    }
}
