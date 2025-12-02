using System;

namespace BookStore.Application.DTOs.Carts;

public class CartItemDto
{
    public CartItemDto(Guid id, Guid bookId,
        int quantity, int unitPrice)
    {
        Id = id;
        BookId = bookId;
        Quantity = quantity;
        UnitPrice = unitPrice;
        TotalPrice = quantity * unitPrice;
    }

    public Guid Id { get; private set; }
    public Guid BookId { get; private set; }
    public int Quantity { get; private set; }
    public int UnitPrice { get; private set; }
    public int TotalPrice { get; private set; }
}