using BookStore.Application.Interface.Common;
using BookStore.Infrastructure.Persistence;

namespace BookStore.Infrastructure.Repository.Common;

public class UnitOfWork : IUnitOfWork
{
    private readonly BookStoreDbContext _dbContext;
    public UnitOfWork(BookStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public void SaveChanges()
    {
        _dbContext.SaveChanges();
    }
}