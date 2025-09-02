using EstateManager.Domain.Abstractions;

namespace EstateManager.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly EstateDbContext _context;

    public UnitOfWork(EstateDbContext context)
    {
        _context = context;
    }

    public async Task<int> CommitAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
