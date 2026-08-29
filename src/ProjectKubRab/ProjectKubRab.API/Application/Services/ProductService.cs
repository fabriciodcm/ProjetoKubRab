using ProjectKubRab.API.Core.Entities;
using ProjectKubRab.API.Core.Interfaces.Application.Services;
using ProjectKubRab.API.Core.Interfaces.Repositories;
using ProjectKubRab.API.Core.Models.Filters;
using ProjectKubRab.API.Core.Models.ViewModels;
using ProjectKubRab.API.Infrastructure.Persistence.Repositories;

namespace ProjectKubRab.API.Application.Services
{
    public class ProductService(IProductRepository productRepository) : IProductService
    {
        
        public async Task<bool> DeleteAsync(string id)
        {
            return await productRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ProductViewModel>> GetAllAsync()
        {
            return (await productRepository.GetAllAsync()).Select(p => (ProductViewModel)p);
        }

        public async Task<ProductViewModel> GetByIdAsync(string id)
        {
            return await productRepository.GetByIdAsync(id);
        }

        public async Task<bool> InsertAsync(ProductViewModel product)
        {
            return await productRepository.InsertAsync(product);
        }

        public async Task<bool> UpdateAsync(ProductViewModel product)
        {
            return await productRepository.UpdateAsync(product);
        }

        public async Task<IEnumerable<ProductViewModel>> GetAllByIntervalAsync(ProductFilter filter)
        {
            var products = await productRepository.getAllByNameAndDateAsync(filter.Name, filter.InitialDate, filter.FinalDate);
            return products.Select(p => (ProductViewModel)p);
        }

        public async Task<bool> RegisterReadingAsync(ProductViewModel product)
        {
            var result = false;
            var productExists = await productRepository.getAllByNameAndDateAsync(product.Name, DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now));
            if (productExists != null && productExists.Any())
            {
                var ExistingEntity = productExists.First();
                var UpdatingEntity = (Product)product;
                UpdatingEntity.Id = ExistingEntity.Id;
                UpdatingEntity.DateAdded = ExistingEntity.DateAdded;
                result = await productRepository.UpdateAsync(UpdatingEntity);
            }
            else
            {
                var entity = (Product)product;
                result = await productRepository.InsertAsync(entity);
            }
            return result;
        }
    }
}
