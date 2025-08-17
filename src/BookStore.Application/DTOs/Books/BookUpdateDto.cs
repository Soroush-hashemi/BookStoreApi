using BookStore.Application.DTOs.ValueObjects;

namespace BookStore.Application.DTOs.Books;

public class BookUpdateDto
{
    public BookUpdateDto(string title, string slug, string pdf, int price,
    string image, string description, string author, Guid categoryId, MetadataDto metadataDto)
    {
        Slug = slug;
        Title = title;
        PDF = pdf;
        Price = price;
        Image = image;
        Author = author;
        Description = description;
        CategoryId = categoryId;
        MetadataDto = metadataDto;
    }

    public Guid Id { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public string Title { get; private set; }
    public string Slug { get; private set; }
    public string Image { get; private set; }
    public string PDF { get; private set; }
    public string Author { get; private set; }
    public string Description { get; private set; }
    public int Price { get; private set; }
    public Guid CategoryId { get; private set; }
    public MetadataDto MetadataDto { get; private set; }
}