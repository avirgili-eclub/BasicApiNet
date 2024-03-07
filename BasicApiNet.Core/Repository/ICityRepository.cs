using BasicApiNet.Core.Dtos;
using BasicApiNet.Core.Models;

namespace BasicApiNet.Core.Repository;

public interface ICityRepository
{
    Task<IEnumerable<City?>> GetCitiesAsync();
    Task<City?> GetCityByIdAsync(int id);
    Task CreateCityAsync(City city);
    Task UpdateCityAsync(CityDto city);
    Task DeleteCityByIdAsync(int id);
}