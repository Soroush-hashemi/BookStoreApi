using BookStore.Domain.Common;
using BookStore.Domain.Exceptions;

namespace BookStore.Domain.Entities.Carts;

public class Cart : BaseEntity
{
    private readonly List<CartItem> _items = new();

    public Guid UserId { get; private set; }
    public bool IsFinaly { get; private set; }
    public IReadOnlyCollection<CartItem> Items => _items.AsReadOnly();
    public int TotalPrice => _items.Sum(i => i.TotalPrice);
    public int TotalItems => _items.Sum(i => i.Quantity);

    public Cart(Guid userId)
    {
        if (userId == Guid.Empty)
            throw new NullPropertyException("userId is invalid");

        UserId = userId;
    }

    public void AddItem(Guid bookId, int quantity, int unitPrice)
    {
        if (bookId == Guid.Empty)
            throw new NullPropertyException("bookId is invalid");

        if (quantity <= 0)
            throw new NullPropertyException("quantity must be greater than zero");

        if (unitPrice < 0)
            throw new NullPropertyException("unitPrice cannot be negative");

        var existing = _items.FirstOrDefault(i => i.BookId == bookId);
        if (existing is not null)
            existing.IncreaseQuantity(quantity);
        else
            _items.Add(new CartItem(bookId, quantity, unitPrice));
    }

    public void AddItem(CartItem item)
    {
        if (item is null)
            throw new ArgumentNullException(nameof(item));

        AddItem(item.BookId, item.Quantity, item.UnitPrice);
    }

    public void RemoveItem(Guid bookId)
    {
        var existing = _items.FirstOrDefault(i => i.BookId == bookId);
        if (existing is not null)
            _items.Remove(existing);
    }

    public void UpdateItemQuantity(Guid bookId, int newQuantity)
    {
        var existing = _items.FirstOrDefault(i => i.BookId == bookId);
        if (existing is null)
            return;

        if (newQuantity <= 0)
        {
            RemoveItem(bookId);
            return;
        }

        existing.UpdateQuantity(newQuantity);
    }

    public void UpdateItemPrice(Guid bookId, int newUnitPrice)
    {
        var existing = _items.FirstOrDefault(i => i.BookId == bookId);
        if (existing is null)
            return;

        existing.UpdateUnitPrice(newUnitPrice);
    }

    public void Finaly()
    {
        IsFinaly = true;
    }

    public void Clear()
    {
        _items.Clear();
    }
}