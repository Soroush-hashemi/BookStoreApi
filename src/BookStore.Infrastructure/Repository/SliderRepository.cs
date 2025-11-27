using BookStore.Application.Interface.Sliders;
using BookStore.Domain.Entities.Slider;
using BookStore.Infrastructure.Persistence;
using BookStore.Infrastructure.Repository.Common;
using BookStore.Shared;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.Repository;

public class SliderRepository : BaseRepository<Slider>, ISliderRepository
{
    private readonly BookStoreDbContext _dbContext;

    public SliderRepository(BookStoreDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Response<List<Slider>>> GetListByDate()
    {
        var Result = await _dbContext.Sliders
            .Where(s => s.IsActive == true)
            .OrderByDescending(s => s.CreatedAt).ToListAsync();

        return Response<List<Slider>>.Ok(Result);
    }
}