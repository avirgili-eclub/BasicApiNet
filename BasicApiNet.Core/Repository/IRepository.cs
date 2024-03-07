using BasicApiNet.Core.Models;

namespace BasicApiNet.Core.Repository;

public interface IRepository<T> where T : BaseEntity
{
    Task<IEnumerable<T?>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task CreateAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteByIdAsync(int id);
    Task SaveChanges();
}
