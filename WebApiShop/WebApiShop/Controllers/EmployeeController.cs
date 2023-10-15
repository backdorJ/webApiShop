using Microsoft.AspNetCore.Mvc;
using WebApiShop.Data.Interfaces;
using WebApiShop.Models;

namespace WebApiShop.Controllers;

[Route("[controller]")]
[ApiController]
public class EmployeeController : Controller
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeController(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    [HttpPost]
    public async Task<IActionResult> CreateEmployee(Employee employee)
    {
        await _employeeRepository.CreateAsync(employee);
        return Ok(employee);
    }
}