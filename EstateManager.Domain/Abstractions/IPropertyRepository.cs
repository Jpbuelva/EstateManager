using EstateManager.Domain.Entities;

namespace EstateManager.Domain.Abstractions;

public interface IPropertyRepository
{
    Task AddAsync(Property property);
    Task<Property?> GetByIdAsync(int id);
    Task<IEnumerable<Property>> GetAllAsync(string? city, decimal? minPrice, decimal? maxPrice);
    Task UpdateAsync(Property property);
}
