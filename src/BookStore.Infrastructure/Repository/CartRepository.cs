using BookStore.Application.Interface;
using BookStore.Domain.Entities.Carts;
using BookStore.Infrastructure.Persistence;
using BookStore.Infrastructure.Repository.Common;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.Repository;

public class CartRepository : BaseRepository<Cart>, ICartRepository
{
    private readonly BookStoreDbContext _dbContext;
    public CartRepository(BookStoreDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Cart> GetByUserIdAsync(Guid userId)
    {
        var cart = await _dbContext.Carts.FirstOrDefaultAsync(u => u.UserId == userId);
        if (cart is null)
            throw new NullReferenceException("cart doesn't Exist");
        return cart;
    }
}