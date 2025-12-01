using System;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Application.DTOs.Carts;

public class CartDto
{
    public CartDto(Guid userId, IEnumerable<CartItemDto> items)
    {
        UserId = userId;
        Items = (items ?? Enumerable.Empty<CartItemDto>()).ToList().AsReadOnly();
        TotalItems = Items.Sum(i => i.Quantity);
        TotalPrice = Items.Sum(i => i.TotalPrice);
    }

    public Guid UserId { get; private set; }
    public IReadOnlyCollection<CartItemDto> Items { get; private set; }
    public int TotalItems { get; private set; }
    public int TotalPrice { get; private set; }
}