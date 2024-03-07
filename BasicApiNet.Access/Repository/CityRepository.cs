using BasicApiNet.Access.Data;
using BasicApiNet.Core.Dtos;
using BasicApiNet.Core.Models;
using BasicApiNet.Core.Repository;
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

    public async Task<City?> GetCityByIdAsync(int id)
    {
        return await _context.Cities.FindAsync(id);
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
        if (existingCity == null)
        {
            throw new InvalidOperationException("City not found");
        }
        
        _context.Entry(existingCity).CurrentValues.SetValues(city);
        
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCityByIdAsync(int id)
    {
        City? city = await _context.Cities.FindAsync(id);
        ArgumentNullException.ThrowIfNull(city);
        _context.Cities.Remove(city);
        await _context.SaveChangesAsync();
    }
}