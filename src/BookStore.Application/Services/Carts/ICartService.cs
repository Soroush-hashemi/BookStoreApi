using BookStore.Application.DTOs.Carts;
using BookStore.Shared;

namespace BookStore.Application.Services.Carts;

public interface ICartService
{
    Task<Result> CreateCart(CartDto cartDto);
    Task<Result> AddItemToCart(Guid userId, CartItemDto CartItemDto);
    Task<Result> DecreaseItemInCart(Guid userId, Guid CartItemId);
    Task<Result> RemoveItemFromCart(Guid CartItemId);

    Task<CartItemDto> GetCartItemById(Guid CartItemId);
    Task<CartDto> GetCartByUserId(Guid CartId);
    Task<CartDto> GetCartById(Guid CartId);
    Task<List<CartDto>> GetCartForAdmin();
}