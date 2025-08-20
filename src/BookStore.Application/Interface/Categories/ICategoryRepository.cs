using System;
using BookStore.Application.Interface.Common;
using BookStore.Domain.Entities.Category;
using BookStore.Shared;

namespace BookStore.Application.Interface.Categories;

public interface ICategoryRepository : IBaseRepository<Category>
{
    Task<Category> GetBySlugAsync(string slug);
    bool SlugExist(string slug);
}
