using BasicApiNet.Access.Data;
using BasicApiNet.Core.Models;
using BasicApiNet.Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace BasicApiNet.Access.Repository;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<T> _entities;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
        _entities = context.Set<T>();
    }

    public async Task<IEnumerable<T?>> GetAllAsync()
    {
        return await _entities.ToListAsync();
        //return await _entities.AsAsyncEnumerable();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _entities.SingleOrDefaultAsync(obj => obj.Id == id);
        //return await _entities.FindAsync(id);
    }

    public async Task CreateAsync(T entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        await _entities.AddAsync(entity);
    }

    public async Task UpdateAsync(T entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        _entities.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteByIdAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        ArgumentNullException.ThrowIfNull(entity);
        _entities.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task SaveChanges()
    {
        await _context.SaveChangesAsync();
    }
}