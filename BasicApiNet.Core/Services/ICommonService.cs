namespace BasicApiNet.Core.Services;

public interface ICommonService<T> where T : class
{
    IEnumerable<T> GetAll();
    T FinById(int Id);
    void Create(T entity);
    void Update(T entity);
    void Delete(T entity);
}