using BookStore.Domain.Common;
using BookStore.Domain.Entities.Book;
using BookStore.Domain.Entities.ValueObjects;
using BookStore.Domain.Exceptions;

namespace BookStore.Domain.Entities.Books;

public class Book : BaseEntity
{
    public string Title { get; private set; }
    public string Slug { get; private set; }
    public string Image { get; private set; }
    public string PDF { get; private set; }
    public string Author { get; private set; }
    public string Description { get; private set; }
    public int Price { get; private set; }
    public Guid CategoryId { get; private set; }
    public Metadata Metadata { get; private set; }

    public Book(string title, string slug, string pdf, int price,
        string image, string description, string author, Guid categoryId,
        Metadata metadata, IBookDomainService domainService)
    {
        var result = domainService.SlugIsExist(slug);
        if (result is true)
            throw new SlugExistExceptions("Slug Exist, Enter new Slug");

        Validation(title, pdf, image, description, author);
        Slug = slug;
        Title = title;
        PDF = pdf;
        Price = price;
        Image = image;
        Author = author;
        Description = description;
        CategoryId = categoryId;
        Metadata = metadata ?? throw new ArgumentNullException(nameof(metadata));
    }

    public void Update(string title, string pdf, int price, Metadata metadata,
        string image, string description, string author, Guid categoryId)
    {
        Validation(title, pdf, image, description, author);

        Title = title;
        PDF = pdf;
        Price = price;
        Image = image;
        Author = author;
        Description = description;
        CategoryId = categoryId;
        Metadata = metadata ?? throw new ArgumentNullException(nameof(metadata));
    }

    public void UpdatePrice(int price)
    {
        Price = price;
    }

    private void Validation(string title, string pdf,
        string image, string description, string author)
    {
        if (string.IsNullOrEmpty(description))
            throw new NullPropertyException("description is null");

        if (string.IsNullOrEmpty(title))
            throw new NullPropertyException("Title is null");

        if (string.IsNullOrEmpty(pdf))
            throw new NullPropertyException("pdf is null");

        if (string.IsNullOrEmpty(image))
            throw new NullPropertyException("image is null");

        if (string.IsNullOrEmpty(author))
            throw new NullPropertyException("author is null");
    }
}