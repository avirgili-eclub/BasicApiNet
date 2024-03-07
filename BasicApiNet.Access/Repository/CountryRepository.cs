using BasicApiNet.Access.Data;
using BasicApiNet.Core.Models;
using BasicApiNet.Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace BasicApiNet.Access.Repository;

public class CountryRepository(ApplicationDbContext context) : ICountryRepository
{
    public async Task<IEnumerable<Country?>> GetCountriesAsync()
    {
        return await context.Countries.ToListAsync();
    }

    public async Task<Country?> GetCountryByIdAsync(int id)
    {
        return await context.Countries.FindAsync(id);
    }

    public async Task CreateCountryAsync(Country country)
    {
        context.Countries.Add(country);
        await context.SaveChangesAsync();
    }

    public async Task UpdateCountryAsync(Country country)
    {
        context.Entry(country).State = EntityState.Modified;
        await context.SaveChangesAsync();
    }
    
    public Task DeleteCountryByIdAsync(int id)
    {
        Country? country = context.Countries.FirstOrDefault(c => c.Id == id);
        ArgumentNullException.ThrowIfNull(country);
        context.Countries.Remove(country);
        context.SaveChanges();
        return Task.CompletedTask;
    }
}