using System;

namespace BookStore.Domain.Entities.Book;

public interface IBookDomainService
{
    bool SlugIsExist(string slug);
}
