using Microsoft.EntityFrameworkCore;
using WebApiShop.Data.Interfaces;
using WebApiShop.Models;

namespace WebApiShop.Data.Repositories;

public class CountryRepository : ICountryRepository
{
    private readonly ApplicationDbContext _dbContext;

    public CountryRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<bool> CreateAsync(Country entity)
    {
        await _dbContext.Countries.AddAsync(entity);
        return await SaveAsync();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _dbContext.Countries.FirstOrDefaultAsync(e => e.Id == id);
        if (entity == null) return false;
        _dbContext.Countries.Remove(entity);
        return await SaveAsync();
    }

    public async Task<ICollection<Country>> GetAllAsync()
    {
        return await _dbContext.Countries.ToListAsync();
    }

    public async Task<Country> GetByIdAsync(int id)
    {
        return await _dbContext.Countries.FirstOrDefaultAsync(x => x.Id == id) ?? new Country();
    }

    public async Task<bool> IsExistAsync(int id)
    {
        return await _dbContext.Countries.AnyAsync(x => x.Id == id);
    }

    public async Task<bool> SaveAsync()
    {
        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<Car>> GetCarsByNameCountryAsync(string countryName)
    {
        return await _dbContext.Countries
            .Where(c => c.Name == countryName)
            .SelectMany(x => x.Cars)
            .ToListAsync();
    }

    public Task<bool> UpdateCountryAsync(Country country)
    {
        _dbContext.Countries.Update(country);
        return SaveAsync();
    }

    public async Task<bool> DeleteCountryAsync(Country country)
    {
        _dbContext.Remove(country);
        return await SaveAsync();
    }
}