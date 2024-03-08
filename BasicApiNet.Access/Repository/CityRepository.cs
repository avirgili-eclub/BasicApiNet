using BasicApiNet.Access.Data;
using BasicApiNet.Core.Dtos;
using BasicApiNet.Core.Models;
using BasicApiNet.Core.Repository;
using BasicApiNet.Middleware.CustomException;
using Microsoft.EntityFrameworkCore;

namespace BasicApiNet.Access.Repository;

public class CityRepository /*(DataContext context)*/ : ICityRepository
{
    private readonly ApplicationDbContext _context;

    public CityRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<City?>> GetCitiesAsync()
    {
        return await _context.Cities.ToListAsync();
    }

    public async Task<City> GetCityByIdAsync(int id)
    {
        var city = await _context.Cities.FindAsync(id);
        NotFoundException.ThrowIfNull(city);
        return city;
    }

    public async Task CreateCityAsync(City city)
    {
        _context.Cities.Add(city);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateCityAsync(CityDto city)
    {
        //  _context.Entry(city).State = EntityState.Modified;
        // await _context.SaveChangesAsync();
        var existingCity = await _context.Cities.FindAsync(city.Id);
        
        NotFoundException.ThrowIfNull(existingCity);
        
        _context.Entry(existingCity).CurrentValues.SetValues(city);
        
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCityByIdAsync(int id)
    {
        City? city = await _context.Cities.FindAsync(id);
        NotFoundException.ThrowIfNull(city);
        _context.Cities.Remove(city);
        await _context.SaveChangesAsync();
    }
}