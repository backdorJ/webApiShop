using WebApiShop.DTO;
using WebApiShop.Models;

namespace WebApiShop.Data.Interfaces;

public interface IProductRepository : IBaseRepository<Product>
{
    Task<IEnumerable<Employee>> GetEmployeesByProductIdAsync(int id);
}