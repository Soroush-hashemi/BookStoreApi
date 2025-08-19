using BookStore.Application.DTOs.Books;
using BookStore.Shared;

namespace BookStore.Application.Services.Books;

public interface IBookService
{
    Task<Result> Create(BookDto dto);
    Task<Result> Update(BookUpdateDto dto);
    Task<Result> UpdatePrice(Guid guid, int price);
    Task<Result> Remove(Guid guid);

    Task<Response<BookDto>> GetById(Guid guid);
    Task<Response<BookDto>> GetBySlug(string Slug);
    Task<Response<List<BookDto>>> GetByAuthor(string Author);
    Task<Response<List<BookDto>>> GetByCategory(string Categoryslug);
    Task<Response<List<BookDto>>> GetByList();
}