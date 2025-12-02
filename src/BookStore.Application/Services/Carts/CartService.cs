using AutoMapper;
using BookStore.Application.DTOs.Carts;
using BookStore.Application.Interface;
using BookStore.Application.Interface.Carts;
using BookStore.Application.Interface.Common;
using BookStore.Domain.Entities.Carts;
using BookStore.Shared;
using FluentValidation;

namespace BookStore.Application.Services.Carts;

public class CartService : ICartService
{
    private readonly ICartRepository _cartRepository;
    private readonly ICartItemRepository _cartItemRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IValidator<CartDto> _validatorCart;
    private readonly IValidator<CartItemDto> _validatorCartItem;
    public CartService(ICartRepository cartRepository, ICartItemRepository cartItemRepository,
        IUnitOfWork unitOfWork, IMapper mapper, IValidator<CartDto> validatorCart,
        IValidator<CartItemDto> validatorCartItem)
    {
        _cartRepository = cartRepository;
        _cartItemRepository = cartItemRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _validatorCart = validatorCart;
        _validatorCartItem = validatorCartItem;
    }

    public async Task<Result> CreateCart(CartDto cartDto)
    {
        try
        {
            var validationCart = await _validatorCart
                .ValidateAsync(cartDto);

            if (!validationCart.IsValid)
                return Result.Error(string.Join
                (",", validationCart.Errors.Select(c => c.ErrorMessage)));

            var cart = new Cart(cartDto.UserId);

            await _cartRepository.CreateAsync(cart);
            _unitOfWork.SaveChanges();

            return Result.Ok();
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }

    public async Task<Result> AddItemToCart(Guid userId, CartItemDto CartItemDto)
    {
        try
        {
            if (userId == Guid.Empty)
                return Result.Error("User Id in Empty");

            var validationItem = await _validatorCartItem
                .ValidateAsync(CartItemDto);

            if (!validationItem.IsValid)
                return Result.Error(string.Join
                (",", validationItem.Errors.Select(i => i.ErrorMessage)));

            var Cart = await _cartRepository
                .GetByUserIdAsync(userId);

            if (Cart is null)
            {
                await CreateCart(new CartDto(userId));
                Cart = await _cartRepository.GetByUserIdAsync(userId);
            }

            var existingItem = await _cartItemRepository
                .ItemIsExistInCart(CartItemDto.Id, CartItemDto.BookId);

            if (existingItem != null)
                existingItem.IncreaseQuantity(1);
            else
                Cart.AddItem(CartItemDto.BookId,
                            CartItemDto.Quantity,
                            CartItemDto.UnitPrice);

            _unitOfWork.SaveChanges();
            return Result.Ok();
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }

    public async Task<Result> DecreaseItemInCart(Guid userId, Guid CartItemId)
    {
        try
        {
            if (userId == Guid.Empty)
                return Result.Error("User Id in Empty");
            if (CartItemId == Guid.Empty)
                return Result.Error("CartItem Id in Empty");

            var Cart = await _cartRepository
                .GetByUserIdAsync(userId);

            if (Cart is null)
            {
                await CreateCart(new CartDto(userId));
                Cart = await _cartRepository
                    .GetByUserIdAsync(userId);
            }

            var Item = await _cartItemRepository
                .GetItemById(CartItemId);

            if (Item is null)
                return Result.Error("item does not exist");

            Item.DecreaseQuantity(1);

            if (Item.Quantity == 0)
                await RemoveItemFromCart(CartItemId);

            _unitOfWork.SaveChanges();
            return Result.Ok();
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }

    public async Task<Result> RemoveItemFromCart(Guid CartItemId)
    {
        try
        {
            if (CartItemId == Guid.Empty)
                return Result.Error("CartItem Id in Empty");

            var CartItem = await _cartItemRepository
                .GetItemById(CartItemId);

            if (CartItem is null)
                return Result.Error("CartItem not found");

            var result = await _cartItemRepository
                .RemoveAsync(CartItemId);

            _unitOfWork.SaveChanges();
            return Result.Ok(result.Message);
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }

    public async Task<CartDto> GetCartById(Guid CartId)
    {
        try
        {
            if (CartId == Guid.Empty)
                throw new Exception("CartId Is Empty");

            var Cart = await _cartRepository
                .GetByIdAsync(CartId);

            var mappedCart = _mapper.Map<CartDto>(Cart);

            return mappedCart;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<CartDto> GetCartByUserId(Guid UserId)
    {
        try
        {
            if (UserId == Guid.Empty)
                throw new Exception("UserId Is Empty");

            var Cart = await _cartRepository.GetByUserIdAsync(UserId);

            var mappedCart = _mapper.Map<CartDto>(Cart);

            return mappedCart;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<CartItemDto> GetCartItemById(Guid CartItemId)
    {
        try
        {
            if (CartItemId == Guid.Empty)
                throw new Exception("CartItemId Is Empty");

            var cartItem = await _cartItemRepository
                .GetByIdAsync(CartItemId);

            var mappedCartItem = _mapper.Map<CartItemDto>(cartItem);

            return mappedCartItem;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<List<CartDto>> GetCartForAdmin()
    {
        try
        {
            var carts = await _cartRepository.GetByListAsync();

            var mappedCarts = _mapper.Map<List<CartDto>>(carts);

            return mappedCarts;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}