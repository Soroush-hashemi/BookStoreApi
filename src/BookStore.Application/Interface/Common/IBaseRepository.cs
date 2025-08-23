using BookStore.Shared;

namespace BookStore.Application.Interface.Common;

public interface IBaseRepository<TEntity> where TEntity : class
{
    Task<Result> CreateAsync(TEntity entity);
    Task<Result> RemoveAsync(Guid guid);

    Task<TEntity> GetByIdAsync(Guid guid);
    Task<List<TEntity>> GetByListAsync();
}