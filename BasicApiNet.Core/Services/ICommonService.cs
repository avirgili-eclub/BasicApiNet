using BasicApiNet.Core.Models;

namespace BasicApiNet.Core.Services;

public interface ICommonService<T> where T : class
{
    IEnumerable<Country> GetAll();
    Task<IReadOnlyList<T>> GetAllAsync();
    Task<T> FinByIdAsync(int id);
    Task CreateAsync(T entity);
    Task<T> UpdateAsync(T country);
    Task<T> DeleteById(int id);
}