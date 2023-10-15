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
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        if (id <= 0) return false;
        var entity = await _dbContext.Employees.FirstOrDefaultAsync(employee => employee.Id == id);
        if (entity == null) return false;
        _dbContext.Remove(entity);
        return true;

    }

    public async Task<IEnumerable<Employee>> GetAllAsync()
    {
        return (await _dbContext.Employees.ToListAsync())!;
    }

    public async Task<Employee> GetByIdAsync(int id)
    {
        return await _dbContext.Employees.FirstOrDefaultAsync(employee => employee.Id == id);
    }
}