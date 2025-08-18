using BookStore.Application.Interface.Common;
using BookStore.Domain.Entities.Books;
using BookStore.Shared;

namespace BookStore.Application.Interface;

public interface IBookRepository : IBaseRepository<Book>
{
    Task<Book> GetBySlugAsync(string slug);
    Task<List<Book>> GetByAuthorAsync(string Author);
    Task<List<Book>> GetByCategoryAsync(string categoryslug);
    bool SlugExist(string slug);
}