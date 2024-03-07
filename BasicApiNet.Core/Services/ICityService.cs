using BasicApiNet.Core.Dtos;
using BasicApiNet.Core.Models;

namespace BasicApiNet.Core.Services;

public interface ICityService
{
    Task<IEnumerable<City?>> GetAllCities();

    Task CreateCity(City city);

    Task<City?> FindCityById(int id);

    void DeleteCityById(int id);

    Task UpdateCity(CityDto city);
}