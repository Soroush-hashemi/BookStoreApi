using BookStore.Application.Interface.Common;
using BookStore.Domain.Entities.Carts;

namespace BookStore.Application.Interface;

public interface ICartRepository : IBaseRepository<Cart>
{
    Task<Cart> GetByUserIdAsync(Guid userId);
}