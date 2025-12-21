namespace BookStore.Domain.Entities.Category;

public interface ICategoryDomainService
{
    bool SlugIsExist(string slug);
}
