using Microsoft.EntityFrameworkCore;
using WebApiShop.Data.Interfaces;
using WebApiShop.DTO;
using WebApiShop.Models;

namespace WebApiShop.Data.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _db;

    public ProductRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<bool> CreateAsync(Product entity)
    {
        await _db.Products.AddAsync(entity);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        if (id <= 0) return false;
        var entity = await _db.Products.FirstOrDefaultAsync(product => product.Id == id);
        if (entity == null) return false;
        _db.Products.Remove(entity);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _db.Products.ToListAsync();
    }

    public async Task<Product> GetByIdAsync(int id)
    {
        if (id <= 0) return new Product();
        var entity = await _db.Products.FirstOrDefaultAsync(x => x.Id == id);
        return entity ?? new Product();
    }

    public async Task<bool> IsExistAsync(int id)
    {
        return await _db.Products.AnyAsync(product => product.Id == id);
    }

    public async Task<IEnumerable<Employee>> GetEmployeesByProductIdAsync(int id)
    {
        return await _db.EmployeeProducts
            .Where(productId => productId.ProductId == id)
            .Select(x => x.Employees)
            .ToListAsync();
    }
}