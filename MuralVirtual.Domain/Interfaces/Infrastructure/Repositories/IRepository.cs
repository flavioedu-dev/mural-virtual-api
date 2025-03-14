using System.Linq.Expressions;

namespace MuralVirtual.Domain.Interfaces.Infrastructure.Repositories;

public interface IRepository<T>
{
    Task<List<T>> GetAllAsync();

    Task<T?> GetAsync(Expression<Func<T, bool>> predicate);

    Task<T?> GetByIdAsync(long id);

    Task CreateAsync(T entity);

    Task UpdateAsync(T entity);

    Task DeleteAsync(long id);
}