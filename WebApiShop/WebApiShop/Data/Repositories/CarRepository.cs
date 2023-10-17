using Microsoft.EntityFrameworkCore;
using WebApiShop.Data.Interfaces;
using WebApiShop.Models;

namespace WebApiShop.Data.Repositories;

public class CarRepository : ICarRepository
{
    private readonly ApplicationDbContext _dbContext;

    public CarRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<bool> CreateAsync(Car entity)
    {
        await _dbContext.Cars.AddAsync(entity);
        return await SaveAsync();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _dbContext.Cars.FirstOrDefaultAsync(x => x.Id == id);
        if (entity == null) return false;
        _dbContext.Cars.Remove(entity);
        return true;
    }

    public async Task<ICollection<Car>> GetAllAsync()
    {
        return await _dbContext.Cars.ToListAsync();
    }

    public async Task<Car> GetByIdAsync(int id)
    {
        return await _dbContext.Cars.FirstOrDefaultAsync(x => x.Id == id) ?? new Car();
    }

    public async Task<bool> IsExistAsync(int id)
    {
        return await _dbContext.Cars.AnyAsync(x => x.Id == id);
    }

    public async Task<bool> SaveAsync()
    {
        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<Country?> GetCountryByNameCarAsync(string nameCar)
    {
        return await _dbContext.Cars
            .Where(x => x.CarName.ToLower() == nameCar.ToLower())
            .Select(x => x.Country)
            .FirstOrDefaultAsync();
    }
}