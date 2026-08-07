using ProjectKubRab.API.Core.Entities;
using ProjectKubRab.API.Core.Interfaces.Repositories;
using ProjectKubRab.API.Core.Interfaces.Services;
using ProjectKubRab.API.Core.Models.Filters;
using ProjectKubRab.API.Core.Models.ViewModels;

namespace ProjectKubRab.API.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductViewModel>> GetAllByIntervalAsync(ProductFilter filter)
        {
            var products = await _productRepository.getAllByNameAndDateAsync(filter.Name, filter.InitialDate, filter.FinalDate);
            return products.Select(p => (ProductViewModel)p);
        }

        public async Task<bool> RegisterReadingAsync(ProductViewModel product)
        {
            var result = false;
            var productExists = await _productRepository.getAllByNameAndDateAsync(product.Name, DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now));
            if(productExists != null && productExists.Any())
            {
                var ExistingEntity = productExists.First();
                var UpdatingEntity = (Product)product;
                UpdatingEntity.Id = ExistingEntity.Id;
                UpdatingEntity.DateAdded = ExistingEntity.DateAdded;
                result = await _productRepository.UpdateAsync(UpdatingEntity);
            }
            else
            {
                var entity = (Product)product;
                result = await _productRepository.InsertAsync(entity);
            }
            return result;
        }
    }
}
