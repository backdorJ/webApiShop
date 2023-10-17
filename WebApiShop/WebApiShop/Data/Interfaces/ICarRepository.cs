using WebApiShop.Models;

namespace WebApiShop.Data.Interfaces;

public interface ICarRepository : IBaseRepository<Car>
{
    Task<Country?> GetCountryByNameCarAsync(string nameCar);
}