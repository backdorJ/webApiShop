using Microsoft.EntityFrameworkCore;
using WebApiShop.Data.Interfaces;
using WebApiShop.Models;

namespace WebApiShop.Data.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly ApplicationDbContext _dbContext;

    public EmployeeRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> CreateAsync(Employee? entity)
    {
        await _dbContext.Employees.AddAsync(entity);
        return await SaveAsync();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        if (id <= 0) return false;
        var entity = await _dbContext.Employees.FirstOrDefaultAsync(employee => employee.Id == id);
        if (entity == null) return false;
        _dbContext.Remove(entity);
        return true;

    }

    public async Task<ICollection<Employee>> GetAllAsync()
    {
        return (await _dbContext.Employees.ToListAsync())!;
    }

    public async Task<Employee> GetByIdAsync(int id)
    {
        return await _dbContext.Employees.FirstOrDefaultAsync(employee => employee.Id == id);
    }

    public async Task<bool> IsExistAsync(int id)
    {
        return await _dbContext.Employees.AnyAsync(employee => employee!.Id == id);
    }

    public async Task<bool> SaveAsync()
    {
        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> CreateEmployeeAsync(int productId, Employee employee)
    {
        var entityProduct = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == productId);

        var employeeProductEntity = new EmployeeProduct() { Employees = employee, Products = entityProduct };

        await _dbContext.EmployeeProducts.AddAsync(employeeProductEntity);
        await _dbContext.Employees.AddAsync(employee);
        return await SaveAsync();
    }
}