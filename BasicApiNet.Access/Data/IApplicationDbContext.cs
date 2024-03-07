using BasicApiNet.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BasicApiNet.Access.Data;

public interface IApplicationDbContext
{
    DbSet<City> Cities { get; set; }
    DbSet<Country> Countries { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}