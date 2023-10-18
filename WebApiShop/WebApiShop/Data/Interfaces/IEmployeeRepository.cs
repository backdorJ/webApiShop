using WebApiShop.Models;

namespace WebApiShop.Data.Interfaces;

public interface IEmployeeRepository : IBaseRepository<Employee>
{
    Task<bool> CreateEmployeeAsync(int productId, Employee employee);
    Task<bool> UpdateEmployeeAsync(Employee employee);
    Task<bool> DeleteEmployeeAsync(Employee employee);
}