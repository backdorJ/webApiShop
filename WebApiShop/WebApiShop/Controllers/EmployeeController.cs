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

    [HttpPost]
    [ProducesResponseType(201, Type = typeof(EmployeeDTO))]
    [ProducesResponseType(500, Type = typeof(EmployeeDTO))]
    [ProducesResponseType(400, Type = typeof(EmployeeDTO))]
    public async Task<IActionResult> CreateEmployee([FromQuery] int productId, [FromBody] EmployeeDTO employee)
    {
        var entity = await _employeeRepository.GetAllAsync();
        var filtered = entity
            .FirstOrDefault(x => x.Name.Trim().ToLower() == employee.Name.Trim().ToLower());

        if (filtered != null)
        {
            ModelState.AddModelError("", "This user already exist!");
            return StatusCode(400, ModelState);
        }

        if (!ModelState.IsValid)
            return BadRequest();

        var employeeFinally = _mapper.Map<Employee>(employee);

        if (!await _employeeRepository.CreateEmployeeAsync(productId, employeeFinally))
        {
            ModelState.AddModelError("", "Something error while saving.");
            return StatusCode(500, ModelState);
        }

        return Ok("Successfully created!");
    }
}