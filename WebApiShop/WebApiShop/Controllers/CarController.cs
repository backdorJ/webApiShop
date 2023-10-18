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
        return Ok(_mapper.Map<List<CarDTO>>(await _carRepository.GetAllAsync()));
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

    [HttpPut("{carId:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> UpdateCarAsync(int carId, [FromBody] CarDTO carDto)
    {
        if (carId == null)
            return BadRequest();

        if (carId != carDto.Id)
            return BadRequest();

        if (!ModelState.IsValid)
            return BadRequest();

        if (!await _carRepository.IsExistAsync(carId))
            return NotFound();

        var car = _mapper.Map<Car>(carDto);
        if (!await _carRepository.UpdateCarAsync(car))
            return BadRequest();
        
        return NoContent();
    }

    [HttpDelete("{carId:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteCarAsync(int carId)
    {
        if (!await _carRepository.IsExistAsync(carId))
            return NotFound();

        if (!ModelState.IsValid)
            return BadRequest();

        var car = await _carRepository.GetByIdAsync(carId);

        if (!await _carRepository.DeleteCarAsync(car))
            return BadRequest();

        return NoContent();
    }
}