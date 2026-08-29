using MongoDB.Driver;
using ProjectKubRab.API.Core.Entities;
using ProjectKubRab.API.Core.Interfaces.Repositories;

namespace ProjectKubRab.API.Infrastructure.Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoCollection<Product> _products;
        public ProductRepository(IMongoDatabase database) { 
            _products = database.GetCollection<Product>("Products");
        }
        public async Task<IEnumerable<Product>> getAllByNameAndDateAsync(string Name, DateOnly? initialDate, DateOnly? finalDate)
        {
            return await _products.Find(p => p.Name == Name 
                && (initialDate == null || (initialDate != null && p.DateAdded >= initialDate))
                && (finalDate == null || (finalDate != null && p.DateAdded <= finalDate))
            ).ToListAsync();
        }

        public async Task<IEnumerable<Product>> getAllByNameAsync(string Name)
        {
            return await _products.Find(p => p.Name == Name).ToListAsync();
        }

        public async Task<bool> UpdateAsync(Product product)
        {
            await _products.UpdateOneAsync(p => p.Id == product.Id, Builders<Product>.Update
                .Set(p => p.Name, product.Name)
                .Set(p => p.Price, product.Price)
                .Set(p => p.DateAdded, product.DateAdded));
            return true;
        }

        public async Task<bool> InsertAsync(Product product)
        {
            await _products.InsertOneAsync(product);
            return true;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var result = await _products.DeleteOneAsync(p => p.Id == Guid.Parse(id));
            return result.DeletedCount > 0;
        }

        public async Task<Product> GetByIdAsync(string id)
        {
            var product = await _products.Find(p => p.Id == Guid.Parse(id)).FirstOrDefaultAsync();
            return product;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _products.Find(_ => true).ToListAsync();
        }
    }
}
