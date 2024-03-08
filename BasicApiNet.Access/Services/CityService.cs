using BasicApiNet.Core.Dtos;
using BasicApiNet.Core.Models;
using BasicApiNet.Core.Repository;
using BasicApiNet.Core.Services;
using Microsoft.Extensions.Logging;

namespace BasicApiNet.Access.Services;

public class CityService(ICityRepository repository, ILogger<CityService> logger) : ICityService
{
    public Task<IEnumerable<City?>> GetAllCities()
    {
        logger.LogInformation("Getting all cities");
        return repository.GetCitiesAsync();
    }

    public Task CreateCity(City city)
    {
        return repository.CreateCityAsync(city);
    }

    public Task<City> FindCityById(int id)
    {
        return repository.GetCityByIdAsync(id);
    }

    public async Task DeleteCityByIdAsync(int id)
    {
        await repository.DeleteCityByIdAsync(id);
    }

    public Task UpdateCity(CityDto city)
    {
        try
        {
            return repository.UpdateCityAsync(city);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error updating city");
            throw;
        }
        
    }

    public IEnumerable<Country> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<City>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<City> FinByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task CreateAsync(City entity)
    {
        throw new NotImplementedException();
    }

    public Task<City> UpdateAsync(City country)
    {
        throw new NotImplementedException();
    }

    public Task<City> DeleteById(int id)
    {
        throw new NotImplementedException();
    }
}