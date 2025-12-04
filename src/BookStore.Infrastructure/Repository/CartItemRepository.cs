using BookStore.Application.Interface.Carts;
using BookStore.Domain.Entities.Carts;
using BookStore.Infrastructure.Persistence;
using BookStore.Infrastructure.Repository.Common;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.Repository;

public class CartItemRepository : BaseRepository<CartItem>, ICartItemRepository
{
    private readonly BookStoreDbContext _dbContext;
    public CartItemRepository(BookStoreDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CartItem> GetItemById(Guid CartItemId)
    {
        var cartItem = await _dbContext.CartItem
            .FirstOrDefaultAsync(c => c.Id == CartItemId);
        if (cartItem is null)
            throw new NullReferenceException("cartItem doesn't Exist");

        return cartItem;
    }

    public async Task<CartItem> ItemIsExistInCart(Guid CartItemId, Guid BookId)
    {
        var cartItem = await _dbContext.CartItem
            .FirstOrDefaultAsync(c => c.Id == CartItemId);

        cartItem

        if (cartItem is null)
            throw new NullReferenceException("cartItem doesn't Exist");

        return cartItem;
    }
}