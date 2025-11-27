using BookStore.Application.Interface.Categories;
using BookStore.Domain.Entities.Category;
using BookStore.Infrastructure.Persistence;
using BookStore.Infrastructure.Repository.Common;
using BookStore.Shared;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.Repository;

public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
    private readonly BookStoreDbContext _dbContext;
    public CategoryRepository(BookStoreDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Category> GetBySlugAsync(string slug)
    {
        var category = await _dbContext.Categories.Where(a => a.Slug == slug).FirstOrDefaultAsync();
        if (category is null)
            throw new NullReferenceException("category doesn't Exist");
        return category;
    }

    public bool SlugExist(string slug)
    {
        var result = _dbContext.Categories.Any(s => s.Slug == slug);
        return result;
    }
}
