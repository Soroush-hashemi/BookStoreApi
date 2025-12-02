using BookStore.Application.Interface.Common;
using BookStore.Domain.Entities.Carts;

namespace BookStore.Application.Interface.Carts;

public interface ICartItemRepository : IBaseRepository<CartItem>
{
    Task<CartItem> ItemIsExistInCart(Guid CartItemId, Guid BookId);
    Task<CartItem> GetItemById(Guid CartItemId);
}
