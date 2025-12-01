using BookStore.Domain.Common;
using BookStore.Domain.Exceptions;

namespace BookStore.Domain.Entities.Carts;

public class CartItem : BaseEntity
{
    public Guid BookId { get; private set; }
    public int Quantity { get; private set; }
    public int UnitPrice { get; private set; }
    public int TotalPrice => Quantity * UnitPrice;

    public CartItem(Guid bookId, int quantity, int unitPrice)
    {
        if (bookId == Guid.Empty)
            throw new NullPropertyException("bookId is invalid");

        if (quantity <= 0)
            throw new NullPropertyException("quantity must be greater than zero");

        if (unitPrice < 0)
            throw new NullPropertyException("unitPrice cannot be negative");

        BookId = bookId;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }

    public void IncreaseQuantity(int amount)
    {
        if (amount <= 0)
            throw new NullPropertyException("amount must be greater than zero");

        Quantity += amount;
    }

    public void UpdateQuantity(int quantity)
    {
        if (quantity <= 0)
            throw new NullPropertyException("quantity must be greater than zero");

        Quantity = quantity;
    }

    public void UpdateUnitPrice(int unitPrice)
    {
        if (unitPrice < 0)
            throw new NullPropertyException("unitPrice cannot be negative");

        UnitPrice = unitPrice;
    }
}