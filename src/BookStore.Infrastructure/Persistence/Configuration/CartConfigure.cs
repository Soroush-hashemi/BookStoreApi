using BookStore.Domain.Entities.Carts;
using BookStore.Infrastructure.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Infrastructure.Persistence.Configuration;

public class CartConfigure : IEntityTypeConfiguration<Cart>
{
    public void Configure(EntityTypeBuilder<Cart> builder)
    {
        builder.HasKey(p => p.Id);

        builder.HasOne<ApplicationUser>()
            .WithMany()
            .HasForeignKey(p => p.UserId)
            .IsRequired();

        builder.Property(p => p.TotalPrice)
            .IsRequired();
        builder.Property(p => p.TotalItems)
            .IsRequired();

        builder.HasMany(c => c.Items)
            .WithOne()
            .HasForeignKey(i => i.CartId)
            .IsRequired();
    }
}