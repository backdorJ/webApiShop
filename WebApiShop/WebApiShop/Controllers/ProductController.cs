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
        var entity = _mapper.Map<List<ProductDTO>>(await _productRepository.GetAllAsync());
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

    [HttpPut("{productId:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> UpdateProductAsync(int productId, [FromBody] ProductDTO productDto)
    {
        if (productId == null)
            return BadRequest();

        if (productId != productDto.Id)
            return BadRequest();

        if (!ModelState.IsValid)
            return BadRequest();

        if (!await _productRepository.IsExistAsync(productId))
            return NotFound();

        var car = _mapper.Map<Product>(productDto);
        if (!await _productRepository.UpdateProductAsync(car))
            return BadRequest();
        
        return NoContent();
    }

    [HttpDelete("{productId:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteProductAsync(int productId)
    {
        if (!await _productRepository.IsExistAsync(productId))
            return NotFound();

        if (!ModelState.IsValid)
            return BadRequest();

        var product = await _productRepository.GetByIdAsync(productId);

        if (!await _productRepository.DeleteProductAsync(product))
            return BadRequest();

        return NoContent();
    }
}