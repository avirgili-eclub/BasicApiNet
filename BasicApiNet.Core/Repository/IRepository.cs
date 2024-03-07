using BasicApiNet.Core.Models;

namespace BasicApiNet.Core.Repository;

public interface IRepository<T> where T : BaseEntity
{
    IQueryable<T?> GetAll();
    Task<IReadOnlyList<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    void CreateAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task DeleteByIdAsync(int id);
    Task SaveChanges();
}
