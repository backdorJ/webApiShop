using WebApiShop.Models;

namespace WebApiShop.Data.Interfaces;

public interface IEmployeeRepository : IBaseRepository<Employee>
{
    Task<bool> CreateEmployeeAsync(int productId, Employee employee);
}