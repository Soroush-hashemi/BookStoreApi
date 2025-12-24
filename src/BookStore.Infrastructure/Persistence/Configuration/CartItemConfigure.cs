
using BookStore.Domain.Entities.Carts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Infrastructure.Persistence.Configuration;

public class CartItemConfigure : IEntityTypeConfiguration<CartItem>
{
    public void Configure(EntityTypeBuilder<CartItem> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(ci => ci.BookId)
               .IsRequired();

        builder.Property(ci => ci.Quantity)
               .IsRequired();

        builder.Property(ci => ci.UnitPrice)
               .IsRequired();

        builder.HasOne<Cart>()
               .WithMany(c => c.Items)
               .HasForeignKey(ci => ci.CartId)
               .IsRequired();
    }
}



// public Guid BookId { get; private set; }
// public Guid CartId { get; private set; }
// public int Quantity { get; private set; }
// public int UnitPrice { get; private set; }
// public int TotalPrice => Quantity * UnitPrice;