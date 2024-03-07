using BasicApiNet.Access.Data;
using BasicApiNet.Core.Models;
using BasicApiNet.Core.Repository;
using BasicApiNet.Core.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BasicApiNet.Access.Services;

public class CountryService : ICommonService<Country>
{
    private readonly IRepository<Country> _repository;
    private readonly ILogger<CountryService> _logger;
    //Se puede implementar un UnitOfWork para evitar el uso del contexto directamente o usar un repository
    //especifico y extender del generico para incluir a Cities
    //_entities.Include(e => ((Country)e).Cities).SingleOrDefaultAsync(obj => obj.Id == id);
    private readonly ApplicationDbContext _context;

    public CountryService(IRepository<Country> repository, ILogger<CountryService> logger, ApplicationDbContext context)
    {
        _repository = repository;
        _logger = logger;
        _context = context;
    }

    public IEnumerable<Country> GetAll()
    {
        _logger.LogInformation("GetAll Countries");
        var countries = _repository.GetAll().Include(c => c.Cities).ToList();
        _logger.LogInformation("Obtain {0} countries", countries.Count());
        return countries;
    }

    public Country FinById(int id)
    {
        _logger.LogInformation("Find Country by Id: {0}", id);
        var country =  _repository.GetByIdAsync(id).Result!;
        if (country != null)
        {
            _context.Entry(country).Collection(c => c.Cities).Load();
        }

        return country;
    }

    public void Create(Country entity)
    {
        try
        {
            _repository.CreateAsync(entity);
            _repository.SaveChanges();
        }
        catch(Exception e)
        {
            _logger.LogError("error creating country: {0}", e.Message);
            throw;
        }
    }

    public void Update(Country entity)
    {
        try
        {
            _repository.UpdateAsync(entity);
        }
        catch (Exception e)
        {
            _logger.LogError("Error updating country: {0}", e.Message);
            throw;
        }
        
    }

    public void Delete(Country entity)
    {
        try
        {
            if(entity != null)
            {
                _repository.DeleteByIdAsync(entity.Id);
                _repository.SaveChanges();
            }
        }
        catch (Exception e)
        {
            _logger.LogError("Error deleting country: {0}", e.Message);
            throw;
        }
    }
}