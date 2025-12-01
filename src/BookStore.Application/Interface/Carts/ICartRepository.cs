using BookStore.Application.Interface.Common;
using BookStore.Domain.Entities.Carts;

namespace BookStore.Application.Interface;

public interface ICartRepository : IBaseRepository<Cart>
{
    Task<Cart?> GetByUserIdAsync(Guid userId);
    Task<bool> UserHasCartAsync(Guid userId);
    Task<Cart> AddOrUpdateItemAsync(Guid userId, CartItem item);
    Task RemoveItemAsync(Guid userId, Guid bookId);
    Task UpdateItemQuantityAsync(Guid userId, Guid bookId, int newQuantity);
    Task UpdateItemPriceAsync(Guid userId, Guid bookId, int newUnitPrice);
    Task ClearCartAsync(Guid userId);
}