using BookStore.Domain.Common;
using BookStore.Domain.Entities.ValueObjects;
using BookStore.Domain.Exceptions;

namespace BookStore.Domain.Entities.Category;

public class Category : BaseEntity
{
    public string Title { get; private set; }
    public string Slug { get; private set; }
    public Metadata Metadata { get; private set; }

    public Category(string title, string slug, Metadata metadata,
        ICategoryDomainService domainService)
    {
        var result = domainService.SlugIsExist(slug);
        if (result is true)
            throw new SlugExistExceptions("Slug Exist, Enter new Slug");

        Validation(title, slug);
        Title = title;
        Slug = slug;
        Metadata = metadata ?? throw new ArgumentNullException(nameof(metadata));
    }

    public void Update(string title, string slug, Metadata metadata)
    {
        Validation(title, slug);
        Title = title;
        Slug = slug;
        Metadata = metadata ?? throw new ArgumentNullException(nameof(metadata));
    }

    private void Validation(string title, string slug)
    {
        if (string.IsNullOrEmpty(title))
            throw new NullPropertyException("title is null");

        if (string.IsNullOrEmpty(slug))
            throw new NullPropertyException("slug is null");
    }
}