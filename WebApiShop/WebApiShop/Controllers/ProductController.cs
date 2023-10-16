using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiShop.Data.Interfaces;
using WebApiShop.DTO;
using WebApiShop.Models;

namespace WebApiShop.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : Controller
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductController(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }
    
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<ProductDTO>))]
    public async Task<IActionResult> GetProducts()
    {
        var entity = _mapper.Map<ProductDTO>(await _productRepository.GetAllAsync());
        return Ok(entity);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(200, Type = typeof(ProductDTO))]
    [ProducesResponseType(404, Type = typeof(ProductDTO))]
    [ProducesResponseType(400, Type = typeof(ProductDTO))]
    public async Task<IActionResult> GetProduct(int id)
    {
        if (!await _productRepository.IsExistAsync(id))
            return NotFound();

        var entity = _mapper.Map<ProductDTO>(await _productRepository.GetByIdAsync(id));
        return Ok(entity);
    }

    [HttpGet("employee/{productId:int}")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<EmployeeDTO>))]
    [ProducesResponseType(404, Type = typeof(IEnumerable<EmployeeDTO>))]
    [ProducesResponseType(400, Type = typeof(IEnumerable<EmployeeDTO>))]
    public async Task<IActionResult> GetEmployeeByProductId(int productId)
    {
        if (!await _productRepository.IsExistAsync(productId))
            return NotFound();

        var entity = _mapper.Map<List<EmployeeDTO>>(await _productRepository.GetEmployeesByProductIdAsync(productId));
        return Ok(entity);
    }
}