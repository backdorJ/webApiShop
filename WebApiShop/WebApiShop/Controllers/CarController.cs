using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiShop.Data.Interfaces;
using WebApiShop.DTO;
using WebApiShop.Models;

namespace WebApiShop.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CarController : Controller
{
    private readonly ICarRepository _carRepository;
    private readonly ICountryRepository _countryRepository;
    private readonly IMapper _mapper;

    public CarController(
        ICarRepository carRepository,
        ICountryRepository  countryRepository,
        IMapper mapper)
    {
        _carRepository = carRepository;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<CarDTO>))]
    public async Task<IActionResult> GetCarsAsync()
    {
        return Ok(_mapper.Map<CarDTO>(await _carRepository.GetAllAsync()));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(200, Type = typeof(CarDTO))]
    [ProducesResponseType(400, Type = typeof(CarDTO))]
    [ProducesResponseType(404, Type = typeof(CarDTO))]
    public async Task<IActionResult> GetCarAsync(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var entity = _mapper.Map<CarDTO>(await _carRepository.GetByIdAsync(id));
        if (entity == null) return NotFound();
        
        return Ok(entity);
    }

    [HttpGet("{carName}")]
    [ProducesResponseType(200, Type = typeof(CountryDTO))]
    [ProducesResponseType(400, Type = typeof(CountryDTO))]
    [ProducesResponseType(404, Type = typeof(CountryDTO))]
    public async Task<IActionResult> GetCountryByNameCarAsync(string carName)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        
        var entity = _mapper.Map<CountryDTO>(await _carRepository.GetCountryByNameCarAsync(carName));
        if (entity == null) return NotFound();

        return Ok(entity);
    }
}