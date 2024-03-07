using BasicApiNet.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BasicApiNet.Access.Data;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    // public DbSet<City> Cities => Set<City>();
    public DbSet<City> Cities { get; set; }
    public DbSet<Country> Countries { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await base.SaveChangesAsync();
    }
}