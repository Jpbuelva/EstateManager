using EstateManager.Application.DTOs;
using EstateManager.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EstateManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertiesController : ControllerBase
{
    private readonly IPropertyService _propertyService;

    public PropertiesController(IPropertyService propertyService)
    {
        _propertyService = propertyService;
    }

    // POST: api/properties
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePropertyDto dto)
    {
        var property = await _propertyService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = property.Id }, property);
    }

    // GET: api/properties
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? city, [FromQuery] decimal? minPrice, [FromQuery] decimal? maxPrice)
    {
        var properties = await _propertyService.GetAllAsync(city, minPrice, maxPrice);
        return Ok(properties);
    }

    // GET: api/properties/{id}
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var property = await _propertyService.GetByIdAsync(id);
        if (property == null) return NotFound();
        return Ok(property);
    }

    // PUT: api/properties/{id}
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdatePropertyDto dto)
    {
        var property = await _propertyService.UpdateAsync(id, dto);
        if (property == null) return NotFound();
        return Ok(property);
    }

    // PATCH: api/properties/{id}/price
    [HttpPatch("{id:int}/price")]
    public async Task<IActionResult> ChangePrice(int id, [FromBody] ChangePriceDto dto)
    {
        var property = await _propertyService.ChangePriceAsync(id, dto);
        if (property == null) return NotFound();
        return Ok(property);
    }

    // POST: api/properties/{id}/images
    [HttpPost("{id:int}/images")]
    public async Task<IActionResult> AddImage(int id, [FromBody] AddImageDto dto)
    {
        var property = await _propertyService.AddImageAsync(id, dto);
        if (property == null) return NotFound();
        return Ok(property);
    }
}
