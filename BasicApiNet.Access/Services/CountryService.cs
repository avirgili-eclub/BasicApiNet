using BasicApiNet.Core.Models;
using BasicApiNet.Core.Repository;
using BasicApiNet.Core.Services;

namespace BasicApiNet.Access.Services;

public class CountryService : ICommonService<Country>
{
    private readonly IRepository<Country> _repository;

    public CountryService(IRepository<Country> repository)
    {
        _repository = repository;
    }

    public IEnumerable<Country> GetAll()
    {
        return _repository.GetAllAsync().Result!;
    }

    public Country FinById(int id)
    {
        return _repository.GetByIdAsync(id).Result!;
    }

    public void Create(Country entity)
    {
        try
        {
            _repository.CreateAsync(entity);
            _repository.SaveChanges();
        }
        catch(Exception)
        {
            //log error
            throw;
        }
    }

    public void Update(Country entity)
    {
        _repository.UpdateAsync(entity);
    }

    public void Delete(Country entity)
    {
        if(entity != null)
        {
            _repository.DeleteByIdAsync(entity.Id);
            _repository.SaveChanges();
        }
    }
}