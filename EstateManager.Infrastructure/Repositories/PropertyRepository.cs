using EstateManager.Domain.Abstractions;
using EstateManager.Domain.Entities;
using EstateManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EstateManager.Infrastructure.Repositories;

public class PropertyRepository : IPropertyRepository
{
    private readonly EstateDbContext _context;

    public PropertyRepository(EstateDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Property property)
    {
        await _context.Properties.AddAsync(property);
    }

    public async Task<Property?> GetByIdAsync(int id)
    {
        return await _context.Properties
            .Include(p => p.Images)
            .FirstOrDefaultAsync(p => p.IdProperty == id);
    }

    public async Task<IEnumerable<Property>> GetAllAsync(string? name, decimal? minPrice, decimal? maxPrice)
    {
        var query = _context.Properties.Include(p => p.Images).AsQueryable();

        if (!string.IsNullOrEmpty(name))
            query = query.Where(p => p.Name == name);

        if (minPrice.HasValue)
            query = query.Where(p => p.Price >= minPrice.Value);

        if (maxPrice.HasValue)
            query = query.Where(p => p.Price <= maxPrice.Value);

        return await query.ToListAsync();
    }

    public async Task UpdateAsync(Property property)
    {
        _context.Properties.Update(property);
        await Task.CompletedTask;
    }
}
