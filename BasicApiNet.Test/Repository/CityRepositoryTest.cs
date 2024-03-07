using BasicApiNet.Access.Data;
using BasicApiNet.Access.Repository;
using BasicApiNet.Core.Dtos;
using BasicApiNet.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BasicApiNet.Test.Repository;

public class CityRepositoryTest
{
    [Test]
    public async Task GetCitiesAsync_ShouldReturnAllCities()
    {
        var dbContext = await GetDatabaseContext();
        var cityRepository = new CityRepository(dbContext);

        var result = await cityRepository.GetCitiesAsync();

        Assert.That(result.Count(), Is.EqualTo(4));
    }
    
    [Test]
    public async Task GetCityByIdAsync_ShouldReturnOneCity()
    {
        const int cityId = 1;

        var dbContext = await GetDatabaseContext();
        var cityRepository = new CityRepository(dbContext);
      
        var result = await cityRepository.GetCityByIdAsync(cityId);
        
        Assert.That(result.Id, Is.EqualTo(cityId));
    }
    
    [Test]
    public async Task CreateCityAsync_ShouldCreateCity()
    {
        var dbContext = await GetDatabaseContext();
        var cityRepository = new CityRepository(dbContext);
        var city = new City { Name = "New York", CountryId = 3};

        await cityRepository.CreateCityAsync(city);
        var result = await cityRepository.GetCityByIdAsync(city.Id);
        
        Assert.That(result.Id, Is.EqualTo(city.Id));
    }
    
    [Test]
    public async Task UpdateCityAsync_ShouldUpdateCity()
    {
        const int cityId = 3;
        var dbContext = await GetDatabaseContext();
        var cityRepository = new CityRepository(dbContext);
        var city = await cityRepository.GetCityByIdAsync(cityId);
        
        CityDto cityDto = new CityDto
        {
            Id = city.Id,
            Name = "Los Angeles"
        };

        await cityRepository.UpdateCityAsync(cityDto);
        var result = await cityRepository.GetCityByIdAsync(cityId);
        
        Assert.That(result.Name, Is.EqualTo("Los Angeles"));
    }
    
    [Test]
    public async Task DeleteCityByIdAsync_ShouldDeleteCity()
    {
        const int cityId = 1;
        var dbContext = await GetDatabaseContext();
        var cityRepository = new CityRepository(dbContext);
        var city = await cityRepository.GetCityByIdAsync(cityId);

        await cityRepository.DeleteCityByIdAsync(city.Id);
        var result = await cityRepository.GetCityByIdAsync(cityId);
        Assert.That(result, Is.Null);
    }
    
    private async Task<ApplicationDbContext> GetDatabaseContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        var dbContext = new ApplicationDbContext(options);
        await dbContext.Database.EnsureCreatedAsync();
        if (await dbContext.Countries.AnyAsync() == false || await dbContext.Cities.AnyAsync() == false)
        {
            var countries = new List<Country>
            {
                new() { Name = "Paraguay" },
                new() { Name = "Argentina" },
                new() { Name = "United States" }
            };

            countries[0].AddCities(new List<City>
            {
                new City { Name = "Asuncion" }
            });
            
            countries[1].AddCities(new List<City>
            {
                new City { Name = "Buenos Aires" },
                new City { Name = "Salta" }
            });
            
            countries[2].AddCities(new List<City>
            {
                new City { Name = "Carlifornia" }
            });

            await dbContext.Countries.AddRangeAsync(countries);
            await dbContext.SaveChangesAsync();
        }
        return dbContext;
    }
}