using BasicApiNet.Access.Data;
using BasicApiNet.Core.Models;

namespace BasicApiNet.Access;

public class Seed
{
     public static async Task SeedData(ApplicationDbContext context)
    {
        if (!context.Countries.Any())
        {
            context.Countries.AddRange(
                new Country { Id = 1, Name = "Paraguay" },
                new Country { Id = 2, Name = "Argentina" },
                new Country { Id = 3, Name = "United States" }
            );

            await context.SaveChangesAsync();

            context.Cities.AddRange(
                new City
                {
                    Id = 1,
                    Name = "Asunción",
                    CountryId = 1
                },
                new City
                {
                    Id = 2,
                    Name = "Lambare",
                    CountryId = 1
                },
                new City
                {
                    Id = 3,
                    Name = "Buenos Aires",
                    CountryId = 2
                },
                new City
                {
                    Id = 4,
                    Name = "New York",
                    CountryId = 3
                }
            );
            
            await context.SaveChangesAsync();
            
        }
    }
}