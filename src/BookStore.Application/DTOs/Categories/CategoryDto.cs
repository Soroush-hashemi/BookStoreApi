using System;
using BookStore.Application.DTOs.ValueObjects;

namespace BookStore.Application.DTOs.Categories;

public class CategoryDto
{
    public CategoryDto(string title, string slug, MetadataDto metadataDto)
    {
        Title = title;
        Slug = slug;
        MetadataDto = metadataDto;
    }

    public string Title { get; private set; }
    public string Slug { get; private set; }
    public MetadataDto MetadataDto { get; private set; }
}
