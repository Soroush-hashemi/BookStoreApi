using BookStore.Application.Interface;
using BookStore.Domain.Entities.Books;
using BookStore.Infrastructure.Persistence;
using BookStore.Infrastructure.Repository.Common;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.Repository;

public class BookRepository : BaseRepository<Book>, IBookRepository
{
    private readonly BookStoreDbContext _dbContext;
    public BookRepository(BookStoreDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Book>> GetByAuthorAsync(string Author)
    {
        var books = await _dbContext.Books.Where(a => a.Author == Author).ToListAsync();
        return books;
    }

    public async Task<List<Book>> GetByCategoryAsync(string categoryslug)
    {
        var books = await _dbContext.Books.Where(a => a.Author == categoryslug).ToListAsync();
        return books;
    }

    public async Task<Book> GetBySlugAsync(string slug)
    {
        var book = await _dbContext.Books.Where(a => a.Slug == slug).FirstOrDefaultAsync();
        if (book is null)
            throw new NullReferenceException("book doesn't Exist");
        return book;
    }

    public bool SlugExist(string slug)
    {
        var result = _dbContext.Books.Any(s => s.Slug == slug);
        return result;
    }
}