using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiShop.Data.Interfaces;
using WebApiShop.DTO;
using WebApiShop.Models;

namespace WebApiShop.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController : Controller
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IMapper _mapper;

    public EmployeeController(IEmployeeRepository employeeRepository, IMapper mapper)
    {
        _employeeRepository = employeeRepository;
        _mapper = mapper;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateEmployee(Employee employee)
    {
        await _employeeRepository.CreateAsync(employee);
        return Ok(employee);
    }

    [ProducesResponseType(200, Type = typeof(EmployeeDTO))]
    [ProducesResponseType(400, Type = typeof(EmployeeDTO))]
    [ProducesResponseType(404, Type = typeof(EmployeeDTO))]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetEmployee(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        if (!await _employeeRepository.IsExistAsync(id))
            return NotFound();
        
        var employee = _mapper.Map<EmployeeDTO>(await _employeeRepository.GetByIdAsync(id));
        return Ok(employee);
    }

    [ProducesResponseType(200, Type = typeof(IEnumerable<EmployeeDTO>))]
    [HttpGet]
    public async Task<IActionResult> GetEmployees()
    {
        var employees = _mapper.Map<List<EmployeeDTO>>(await _employeeRepository.GetAllAsync());
        return Ok(employees);
    }
}