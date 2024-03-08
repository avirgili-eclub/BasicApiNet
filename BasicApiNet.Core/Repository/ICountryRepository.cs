using BasicApiNet.Core.Models;

namespace BasicApiNet.Core.Repository;

public interface ICountryRepository
{
    Task<IEnumerable<Country?>> GetCountriesAsync();
    Task<Country> GetCountryByIdAsync(int id);
    Task CreateCountryAsync(Country country);
    Task UpdateCountryAsync(Country country);
    Task DeleteCountryByIdAsync(int id);
}