using System;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Application.DTOs.Carts;

public class CartDto
{
    public CartDto(Guid userId)
    {
        UserId = userId;
        Items = new List<CartItemDto>();
        TotalItems = Items.Sum(i => i.Quantity);
        TotalPrice = Items.Sum(i => i.TotalPrice);
    }

    public Guid UserId { get; private set; }
    public List<CartItemDto> Items { get; private set; }
    public int TotalItems { get; private set; }
    public int TotalPrice { get; private set; }
}