using ProjectKubRab.API.Core.Entities;

namespace ProjectKubRab.API.Core.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> getAllByNameAsync(string Name);
        Task<IEnumerable<Product>> getAllByNameAndDateAsync(string Name, DateOnly? initialDate, DateOnly? finalDate);
        Task<bool> UpdateAsync(Product product);
        Task<bool> InsertAsync(Product product);
        Task<bool> DeleteAsync(string id);
        Task<Product> GetByIdAsync(string id);
        Task<IEnumerable<Product>> GetAllAsync();
    }
}
