namespace MuralVirtual.Domain.Interfaces.Infrastructure.Repositories;

public interface IUnitOfWork : IDisposable
{
    IUserRepository UserRepository { get; }

    Task CommitAsync();
}
