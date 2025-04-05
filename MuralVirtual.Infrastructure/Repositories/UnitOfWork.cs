using MuralVirtual.Domain.Interfaces.Infrastructure.Repositories;

namespace MuralVirtual.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly MuralVirtualDbContext _context;

    private IUserRepository? _userRepository;

    public UnitOfWork(MuralVirtualDbContext context)
    {
        _context = context;
    }

    public IUserRepository UserRepository
    {
        get => _userRepository ??= new UserRepository(_context);
    }

    public async Task CommitAsync() => await _context.SaveChangesAsync();

    public void Dispose() => _context.Dispose();
}
