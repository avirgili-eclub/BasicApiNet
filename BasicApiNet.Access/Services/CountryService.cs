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

    public async Task<IReadOnlyList<Country>> GetAllAsync()
    {
        _logger.LogInformation("GetAllAsync Countries");
        var countries = await _repository.GetAllAsync();
        _logger.LogInformation("Obtain {0} countries", countries.Count());
        return countries;
    }

    public async Task<Country> FinByIdAsync(int id)
    {
        try
        {
            _logger.LogInformation("Find Country by Id: {0}", id);
            var country = await _repository.GetByIdAsync(id);
            if (country == null)
            {
                throw new Exception("Country not found");
            }
            _context.Entry(country).Collection(c => c.Cities).Load();
            return country;
        }
        catch (Exception e)
        {
            _logger.LogError("Error finding country by id: {0}", e.Message);
            throw;
        }
    }

    public async Task CreateAsync(Country entity)
    {
        try
        {
            _repository.CreateAsync(entity);
            await _repository.SaveChanges();
        }
        catch (Exception e)
        {
            _logger.LogError("error creating country: {0}", e.Message);
            throw;
        }
    }

    public async Task<Country> UpdateAsync(Country country)
    {
        try
        {
            var result = await _repository.UpdateAsync(country);
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError("Error updating country: {0}", e.Message);
            throw;
        }
    }

    public void DeleteById(int id)
    {
        try
        {
            var country = _repository.GetByIdAsync(id);
            if (country == null)
            {
                throw new Exception("Country not found");
            }

            _repository.DeleteByIdAsync(id);
            _repository.SaveChanges();
        }
        catch (Exception e)
        {
            _logger.LogError("Error deleting country: {0}", e.Message);
            throw;
        }
    }
}