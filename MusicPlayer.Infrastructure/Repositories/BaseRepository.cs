using Microsoft.EntityFrameworkCore;
using MusicPlayer.Core.Interfaces.IRepositories;
using MusicPlayer.Infrastructure.Data;

namespace MusicPlayer.Infrastructure.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    protected readonly MusicPlayerDbContext _dbContext;
    protected DbSet<T> _dbSet => _dbContext.Set<T>();

    public BaseRepository(MusicPlayerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<T>> GetAllAsync(int page = 0, int pageSize = 0)
    {
        var data = await _dbSet
            .AsNoTracking()
            .ToListAsync();

        return data;
    }

    public async Task<T> GetByIdAsync<Tid>(Tid id)
    {
        var data = await _dbSet.FindAsync(id);
        if (data == null)
        {
            throw new KeyNotFoundException($"Entity with ID {id} not found.");
        }
        return data;
    }

    public async Task<T> CreateAsync(T model)
    {
        await _dbSet.AddAsync(model);
        await _dbContext.SaveChangesAsync();

        return model;
    }

    public async Task UpdateAsync(T model)
    {
        _dbSet.Update(model);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(T model)
    {
        _dbSet.Remove(model);
        await _dbContext.SaveChangesAsync();
    }

    public async Task SaveChangeAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}
