using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiShop.Data.Interfaces;
using WebApiShop.DTO;
using WebApiShop.Models;

namespace WebApiShop.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CountryController : Controller
{
    private readonly ICountryRepository _countryRepository;
    private readonly IMapper _mapper;

    public CountryController(ICountryRepository countryRepository, IMapper mapper)
    {
        _countryRepository = countryRepository;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<CountryDTO>))]
    public async Task<IActionResult> GetCountries()
    {
        var entities = _mapper.Map<List<CountryDTO>>(await _countryRepository.GetAllAsync());
        return Ok(entities);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(200, Type = typeof(CountryDTO))]
    [ProducesResponseType(400, Type = typeof(CountryDTO))]
    [ProducesResponseType(404, Type = typeof(CountryDTO))]
    public async Task<IActionResult> GetCountry(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        if (!await _countryRepository.IsExistAsync(id))
            return NotFound();

        var entity = _mapper.Map<CountryDTO>(await _countryRepository.GetByIdAsync(id));
        
        return Ok(entity);
    }
    
    [HttpGet("cars/{countryName}")]
    [ProducesResponseType(200, Type = typeof(List<CarDTO>))]
    [ProducesResponseType(400, Type = typeof(List<CarDTO>))]
    [ProducesResponseType(404, Type = typeof(List<CarDTO>))]
    public async Task<IActionResult> GetCarByCountryName(string countryName)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var entity = _mapper.Map<List<CarDTO>>(await _countryRepository.GetCarsByNameCountryAsync(countryName));
        return Ok(entity);
    }

    [HttpPost]
    [ProducesResponseType(201, Type = typeof(CountryDTO))]
    [ProducesResponseType(400, Type = typeof(CountryDTO))]
    [ProducesResponseType(500, Type = typeof(CountryDTO))]
    public async Task<IActionResult> CreateCountry([FromBody] CountryDTO country)
    {
        var entity = await _countryRepository.GetAllAsync();
        var filtered = entity
            .FirstOrDefault(x => x.Name.Trim().ToLower() == country.Name.Trim().ToLower());

        if (filtered != null)
        {
            ModelState.AddModelError("", "This country already exists!");
            return StatusCode(400, ModelState);
        }

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var countryMap = _mapper.Map<Country>(country);
        if (!await _countryRepository.CreateAsync(countryMap))
        {
            ModelState.AddModelError("", "Something went error while saving!");
            return StatusCode(500, ModelState);
        }
        
        return Ok("Successfully created!");
    }
}