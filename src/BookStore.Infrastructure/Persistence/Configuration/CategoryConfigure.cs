using BookStore.Domain.Entities.Category;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Infrastructure.Persistence.Configuration;

public class CategoryConfigure : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Title)
            .IsRequired().HasMaxLength(200);

        builder.Property(p => p.Slug).IsUnicode()
            .IsRequired().HasMaxLength(200);

        builder.OwnsOne(b => b.Metadata, metadata =>
        {
            metadata.Property(m => m.Title).HasMaxLength(200);
            metadata.Property(m => m.Description).HasMaxLength(500);
            metadata.Property(m => m.Keyword).HasMaxLength(200);
            metadata.Property(m => m.CanonicalUrl).HasMaxLength(500);
            metadata.Property(m => m.IndexPage).HasMaxLength(50);
            metadata.Property(m => m.OgTitle).HasMaxLength(200);
            metadata.Property(m => m.OgDescription).HasMaxLength(500);
            metadata.Property(m => m.OgUrl).HasMaxLength(500);
            metadata.Property(m => m.OgImage).HasMaxLength(1000);
        });
    }
}
