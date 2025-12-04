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
}