using BookStore.Application.Interface.Common;
using BookStore.Domain.Entities.Slider;
using BookStore.Shared;

namespace BookStore.Application.Interface.Sliders;

public interface ISliderRepository : IBaseRepository<Slider>
{
    Task<Response<List<Slider>>> GetListByDate();
}