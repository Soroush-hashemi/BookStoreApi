using BookStore.Application.Interface;
using BookStore.Domain.Entities.Book;

namespace BookStore.Application.Services.Books;

public class BookDomainService : IBookDomainService
{
    private readonly IBookRepository _bookRepository;
    public BookDomainService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public bool SlugIsExist(string slug)
    {
        var Result = _bookRepository.SlugExist(slug);
        return Result;
    }
}
