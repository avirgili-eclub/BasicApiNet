using BasicApiNet.Core.Dtos;
using BasicApiNet.Core.Models;
using BasicApiNet.Core.Repository;
using BasicApiNet.Core.Services;
using Microsoft.Extensions.Logging;

namespace BasicApiNet.Access.Services;

public class CityService(ICityRepository repository, ILogger<CityService> logger) : ICityService
{
    private readonly ILogger<CityService> _logger = logger;

    public Task<IEnumerable<City?>> GetAllCities()
    {
        _logger.LogInformation("Getting all cities");
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
            _logger.LogError(e, "Error updating city");
            throw;
        }
        
    }
}