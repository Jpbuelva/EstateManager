using EstateManager.Application.DTOs;

namespace EstateManager.Application.Interfaces;

public interface IPropertyService
{
    Task<PropertyDto> CreateAsync(CreatePropertyDto dto);
    Task<PropertyDto?> UpdateAsync(int id, UpdatePropertyDto dto);
    Task<PropertyDto?> ChangePriceAsync(int id, ChangePriceDto dto);
    Task<PropertyDto?> AddImageAsync(int propertyId, AddImageDto dto);
    Task<IEnumerable<PropertyDto>> GetAllAsync(string? city, decimal? minPrice, decimal? maxPrice);
    Task<PropertyDto?> GetByIdAsync(int id);
}
