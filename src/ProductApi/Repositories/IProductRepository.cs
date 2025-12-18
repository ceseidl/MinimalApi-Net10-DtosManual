using ProductApi.Domain;

namespace ProductApi.Repositories;

public interface IProductRepository
{
    Task<IReadOnlyList<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(Guid id);
    Task<Product> CreateAsync(Product entity);
    Task UpdateAsync(Product entity);
    Task DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
}
