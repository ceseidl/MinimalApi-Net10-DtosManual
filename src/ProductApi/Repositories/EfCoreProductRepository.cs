using Microsoft.EntityFrameworkCore;
using ProductApi.Data;
using ProductApi.Domain;

namespace ProductApi.Repositories;

public sealed class EfCoreProductRepository : IProductRepository
{
    private readonly AppDbContext _db;
    public EfCoreProductRepository(AppDbContext db) => _db = db;

    public async Task<IReadOnlyList<Product>> GetAllAsync()
        => await _db.Products.AsNoTracking().OrderBy(p => p.Name).ToListAsync();

    public async Task<Product?> GetByIdAsync(Guid id)
        => await _db.Products.FindAsync(id);

    public async Task<Product> CreateAsync(Product entity)
    {
        entity.Id = Guid.NewGuid();
        _db.Products.Add(entity);
        await _db.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(Product entity)
    {
        _db.Products.Update(entity);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _db.Products.FindAsync(id);
        if (entity is null) return;
        _db.Products.Remove(entity);
        await _db.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(Guid id)
        => await _db.Products.AnyAsync(p => p.Id == id);
}
