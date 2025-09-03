using BookStore.Application.Interface.Common;
using BookStore.Infrastructure.Persistence;
using BookStore.Shared;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.Repository.Common;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    private readonly BookStoreDbContext _dbContext;
    public BaseRepository(BookStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result> CreateAsync(TEntity entity)
    {
        var result = await _dbContext.Set<TEntity>().AddAsync(entity);
        return Result.Ok();
    }

    public async Task<Result> RemoveAsync(Guid guid)
    {
        var entity = await GetByIdAsync(guid);
        _dbContext.Set<TEntity>().Remove(entity);

        return Result.Ok();
    }

    public async Task<TEntity> GetByIdAsync(Guid guid)
    {
        var result = await _dbContext.Set<TEntity>().FindAsync(guid);
        if (result is null)
            throw new NullReferenceException("doesn't Exist");

        return result;
    }

    public async Task<List<TEntity>> GetByListAsync()
    {
        var result = await _dbContext.Set<TEntity>().ToListAsync();
        return result;

    }
}