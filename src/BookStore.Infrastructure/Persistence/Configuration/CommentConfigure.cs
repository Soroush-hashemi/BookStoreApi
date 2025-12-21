using BookStore.Domain.Entities.Comments;
using BookStore.Domain.Entities.Books;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BookStore.Infrastructure.Identity.Data;

namespace BookStore.Infrastructure.Persistence.Configuration;

public class CommentConfigure : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Content)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(c => c.AuthorName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.Status)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(c => c.BookId)
            .IsRequired();

        builder.HasOne<ApplicationUser>()
            .WithMany()
            .HasForeignKey(c => c.UserId)
            .IsRequired();

        builder.HasOne<Book>()
            .WithMany()
            .HasForeignKey(c => c.BookId);

        // indexes
        builder.HasIndex(c => c.BookId);
        builder.HasIndex(c => c.Status);
        builder.HasIndex(c => new { c.BookId, c.Status });
    }
}