using BookStore.Application.DTOs.Categories;
using BookStore.Shared;

namespace BookStore.Application.Services.Categories;

public interface ICategoryService
{
    Task<Result> Create(CategoryDto dto);
    Task<Result> Update(CategoryUpdateDto dto);
    Task<Result> Remove(Guid guid);

    Task<Response<CategoryDto>> GetById(Guid guid);
    Task<Response<CategoryDto>> GetBySlug(string Slug);
    Task<Response<List<CategoryDto>>> GetByList();
}