using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiShop.Data.Interfaces;
using WebApiShop.DTO;
using WebApiShop.Models;

namespace WebApiShop.Controllers;

[Authorize]
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

    [HttpPut("{employeeId:int}")]
    [ProducesResponseType(400)]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> PutEmployeeAsync(int employeeId, [FromBody] EmployeeDTO? employeeDto)
    {
        if (employeeDto == null)
            return BadRequest(ModelState);

        if (employeeId != employeeDto.Id)
            return NotFound();

        if (!await _employeeRepository.IsExistAsync(employeeId))
            return NotFound();

        if (!ModelState.IsValid)
            return BadRequest();

        var employee = _mapper.Map<Employee>(employeeDto);

        if (!await _employeeRepository.UpdateEmployeeAsync(employee))
            return BadRequest();

        return NoContent();
    }

    [HttpDelete("{employeeId:int}")]
    [ProducesResponseType(400)]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteEmployeeAsync(int employeeId)
    {
        if (!await _employeeRepository.IsExistAsync(employeeId))
            return NotFound();

        if (!ModelState.IsValid)
            return BadRequest();

        var employee = await _employeeRepository.GetByIdAsync(employeeId);

        if (!await _employeeRepository.DeleteEmployeeAsync(employee))
            return BadRequest();

        return NoContent();
    }
}