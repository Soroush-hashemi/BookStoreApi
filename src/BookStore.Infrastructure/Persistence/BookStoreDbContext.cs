using BookStore.Domain.Entities.Books;
using BookStore.Domain.Entities.Carts;
using BookStore.Domain.Entities.Category;
using BookStore.Domain.Entities.Comments;
using BookStore.Domain.Entities.Slider;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.Persistence;

public class BookStoreDbContext : DbContext
{
    public BookStoreDbContext
        (DbContextOptions<BookStoreDbContext> options) : base(options)
    {
    }

    public DbSet<Book> Books { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Slider> Sliders { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItem { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}