using BasicApiNet.Access.Data;
using BasicApiNet.Access.Repository;
using BasicApiNet.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BasicApiNet.Test.Repository;

public class CountryRepositoryTest
{
    [Test]
    public async Task GetCountriesAsync_ShouldReturnAllCountries()
    {
        var dbContext = await GetDatabaseContext();
        var countryRepository = new CountryRepository(dbContext);

        var result = await countryRepository.GetCountriesAsync();

        Assert.That(result.Count(), Is.EqualTo(3));
    }

    [Test]
    public async Task GetCountryByIdAsync_ShouldReturnCountry()
    {
        const int countryId = 1;

        var dbContext = await GetDatabaseContext();
        var countryRepository = new CountryRepository(dbContext);
      
        var result = await countryRepository.GetCountryByIdAsync(countryId);
        
        Assert.That(result.Id, Is.EqualTo(countryId));
    }
    
    [Test]
    public async Task CreateCountryAsync_ShouldCreateCountry()
    {
        var dbContext = await GetDatabaseContext();
        var countryRepository = new CountryRepository(dbContext);
        var country = new Country { Name = "Brazil" };

        await countryRepository.CreateCountryAsync(country);
        var result = await countryRepository.GetCountryByIdAsync(country.Id);
        
        Assert.That(result.Id, Is.EqualTo(country.Id));
    }
    
    [Test]
    public async Task UpdateCountryAsync_ShouldUpdateCountry()
    {
        const int countryId = 3;
        var dbContext = await GetDatabaseContext();
        var countryRepository = new CountryRepository(dbContext);
        var country = await countryRepository.GetCountryByIdAsync(countryId);
        country.Name = "Uruguay";

        await countryRepository.UpdateCountryAsync(country);
        var result = await countryRepository.GetCountryByIdAsync(countryId);
        
        Assert.That(result.Name, Is.EqualTo("Uruguay"));
    }
    
    [Test]
    public async Task DeleteCountryByIdAsync_ShouldDeleteCountry()
    {
        const int countryId = 1;
        var dbContext = await GetDatabaseContext();
        var countryRepository = new CountryRepository(dbContext);
        var country = await countryRepository.GetCountryByIdAsync(countryId);

        await countryRepository.DeleteCountryByIdAsync(country.Id);
        var result = await countryRepository.GetCountryByIdAsync(countryId);
        
        Assert.IsNull(result);
    }
    
    private async Task<ApplicationDbContext> GetDatabaseContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        var dbContext = new ApplicationDbContext(options);
        await dbContext.Database.EnsureCreatedAsync();
        if (await dbContext.Countries.AnyAsync() == false)
        {
            var countries = new List<Country>
            {
                new() { Name = "Paraguay" },
                new() { Name = "Argentina" },
                new() { Name = "United States" }
            };

            countries[0].AddCities(new List<City>
            {
                new City { Name = "Asuncion" },
                new City { Name = "Encarnacion" },
                new City { Name = "Ciudad del Este" }
            });
            
            countries[1].AddCities(new List<City>
            {
                new City { Name = "Buenos Aires" },
                new City { Name = "Rosario" },
                new City { Name = "Cordoba" }
            });
            
            countries[2].AddCities(new List<City>
            {
                new City { Name = "New York" },
                new City { Name = "Los Angeles" },
                new City { Name = "Carlifornia" }
            });

            await dbContext.Countries.AddRangeAsync(countries);
            await dbContext.SaveChangesAsync();
        }

        return dbContext;
    }

}