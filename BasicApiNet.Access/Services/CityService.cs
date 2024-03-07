using BasicApiNet.Core.Models;
using BasicApiNet.Core.Repository;
using BasicApiNet.Core.Services;

namespace BasicApiNet.Access.Services;

public class CityService(ICityRepository repository) : ICityService
{
    public Task<IEnumerable<City?>> GetAllCities()
    {
        return repository.GetCitiesAsync();
    }

    public Task CreateCity(City city)
    {
        return repository.CreateCityAsync(city);
    }

    public Task<City?> FindCityById(int id)
    {
        return repository.GetCityByIdAsync(id);
    }

    public void DeleteCityById(int id)
    {
        repository.DeleteCityByIdAsync(id);
    }

    public Task UpdateCity(City city)
    {
        return repository.UpdateCityAsync(city);
    }
}