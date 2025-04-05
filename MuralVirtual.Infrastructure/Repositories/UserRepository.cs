using MuralVirtual.Domain.Entities;
using MuralVirtual.Domain.Interfaces.Infrastructure.Repositories;

namespace MuralVirtual.Infrastructure.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(MuralVirtualDbContext context)
    {
        _context = context;
    }
}
