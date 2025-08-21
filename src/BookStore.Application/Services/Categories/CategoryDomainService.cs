using BookStore.Application.Interface.Categories;
using BookStore.Domain.Entities.Category;

namespace BookStore.Application.Services.Categories;

public class CategoryDomainService : ICategoryDomainService
{
    private readonly ICategoryRepository _categoryRepository;
    public CategoryDomainService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }
    public bool SlugIsExist(string slug)
    {
        var result = _categoryRepository.SlugExist(slug);
        return result;
    }
}