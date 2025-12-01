using System;

namespace BookStore.Application.DTOs.Carts;

public class CartItemDto
{
    public CartItemDto(Guid bookId, int quantity, int unitPrice)
    {
        BookId = bookId;
        Quantity = quantity;
        UnitPrice = unitPrice;
        TotalPrice = quantity * unitPrice;
    }

    public Guid BookId { get; private set; }
    public int Quantity { get; private set; }
    public int UnitPrice { get; private set; }
    public int TotalPrice { get; private set; }
}