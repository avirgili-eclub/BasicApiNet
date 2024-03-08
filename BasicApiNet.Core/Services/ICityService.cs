using BasicApiNet.Core.Dtos;
using BasicApiNet.Core.Models;

namespace BasicApiNet.Core.Services;

public interface ICityService : ICommonService<City>
{
    Task<IEnumerable<City?>> GetAllCities();

    Task CreateCity(City city);

    Task<City> FindCityById(int id);

    Task DeleteCityByIdAsync(int id);

    Task UpdateCity(CityDto city);
}