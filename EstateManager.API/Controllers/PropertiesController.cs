using EstateManager.Application.DTOs;
using EstateManager.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EstateManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
//[Authorize]
public class PropertiesController : ControllerBase
{
    private readonly IPropertyService _propertyService;

    public PropertiesController(IPropertyService propertyService)
    {
        _propertyService = propertyService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreatePropertyDto dto)
    {
        var property = await _propertyService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = property.IdProperty }, property);
    }

   
    [HttpGet("getbyfilter")]
    public async Task<IActionResult> GetAll([FromQuery] string? name, [FromQuery] decimal? minPrice, [FromQuery] decimal? maxPrice)
    {
        var properties = await _propertyService.GetAllAsync(name, minPrice, maxPrice);
        return Ok(properties);
    }

   
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var property = await _propertyService.GetByIdAsync(id);
        if (property == null) return NotFound();
        return Ok(property);
    }

     
    [HttpPut("update")]
    public async Task<IActionResult> Update([FromBody] UpdatePropertyDto dto)
    {
        var property = await _propertyService.UpdateAsync(dto);
        if (property == null) return NotFound();
        return Ok(property);
    }

 
    [HttpPatch("updateprice")]
    public async Task<IActionResult> ChangePrice([FromBody] ChangePriceDto dto)
    {
        var property = await _propertyService.ChangePriceAsync(dto);
        if (property == null) return NotFound();
        return Ok(property);
    }

    [HttpPost("{id:int}/images")]
    public async Task<IActionResult> AddImage(int id, [FromForm] AddImageDto dto)
    {
        var property = await _propertyService.AddImageAsync(id, dto);
        if (property == null) return NotFound();

        return Ok(property);
    }
}
