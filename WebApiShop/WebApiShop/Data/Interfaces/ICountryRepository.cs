using WebApiShop.Models;

namespace WebApiShop.Data.Interfaces;

public interface ICountryRepository : IBaseRepository<Country>
{
    Task<IEnumerable<Car>> GetCarsByNameCountryAsync(string countryName);
}