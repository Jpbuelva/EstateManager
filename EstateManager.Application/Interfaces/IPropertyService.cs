using EstateManager.Application.DTOs;

namespace EstateManager.Application.Interfaces;

public interface IPropertyService
{
    Task<PropertyDto> CreateAsync(CreatePropertyDto dto);
    Task<PropertyDto?> UpdateAsync(UpdatePropertyDto dto);
    Task<PropertyDto?> ChangePriceAsync(ChangePriceDto dto);
    Task<PropertyDto?> AddImageAsync(int propertyId, AddImageDto dto);
    Task<IEnumerable<PropertyDto>> GetAllAsync(string? city, decimal? minPrice, decimal? maxPrice);
    Task<PropertyDto?> GetByIdAsync(int id);
}
