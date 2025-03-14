using Microsoft.EntityFrameworkCore;
using MuralVirtual.Domain.Interfaces.Infrastructure.Repositories;
using System.Linq.Expressions;

namespace MuralVirtual.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected MuralVirtualDbContext? _context;

    public async Task<T?> GetAsync(Expression<Func<T, bool>> predicate)
        => await _context!.Set<T>().FindAsync(predicate);

    public async Task<T?> GetByIdAsync(long id)
        => await _context!.Set<T>().FindAsync(id);

    public async Task<List<T>> GetAllAsync()
        => await _context!.Set<T>().ToListAsync();

    public async Task CreateAsync(T entity)
        => await _context!.Set<T>().AddAsync(entity);

    public Task UpdateAsync(T entity)
    {
        _context!.Set<T>().Update(entity);

        return Task.CompletedTask;
    }

    public async Task DeleteAsync(long id)
    {
        T? entity = await GetByIdAsync(id);

        if (entity is null)
            return;

        _context!.Set<T>().Remove(entity);
    }
}