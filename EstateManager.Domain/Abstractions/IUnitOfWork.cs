namespace EstateManager.Domain.Abstractions;

public interface IUnitOfWork
{
     
    Task<int> CommitAsync();
}
