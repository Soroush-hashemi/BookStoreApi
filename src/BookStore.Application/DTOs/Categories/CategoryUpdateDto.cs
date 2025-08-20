using System;
using BookStore.Application.DTOs.ValueObjects;

namespace BookStore.Application.DTOs.Categories;

public class CategoryUpdateDto
{
    public CategoryUpdateDto(string title, string slug, MetadataDto metadataDto)
    {
        Title = title;
        Slug = slug;
        MetadataDto = metadataDto;
    }

    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string Slug { get; private set; }
    public MetadataDto MetadataDto { get; private set; }
}
