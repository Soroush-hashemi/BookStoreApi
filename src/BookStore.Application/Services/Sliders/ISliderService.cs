using BookStore.Application.DTOs.Sliders;
using BookStore.Shared;

namespace BookStore.Application.Services.Sliders;

public interface ISliderService
{
    Task<Result> Create(SliderDto sliderDto);
    Task<Result> Delete(Guid guid);

    Task<Response<SliderDto>> GetById(Guid guid);
    Task<Response<List<SliderDto>>> GetList();
    Task<Response<List<SliderDto>>> GetListByDate();
}
