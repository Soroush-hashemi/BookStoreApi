using AutoMapper;
using BookStore.Application.DTOs.Sliders;
using BookStore.Application.Interface.Common;
using BookStore.Application.Interface.Sliders;
using BookStore.Domain.Entities.Slider;
using BookStore.Shared;
using FluentValidation;

namespace BookStore.Application.Services.Sliders;

public class SliderService : ISliderService
{
    private readonly ISliderRepository _sliderRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<SliderDto> _validator;
    private readonly IMapper _mapper;
    public SliderService(ISliderRepository sliderRepository, IMapper mapper,
        IUnitOfWork unitOfWork, IValidator<SliderDto> validator)
    {
        _sliderRepository = sliderRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
        _mapper = mapper;
    }

    public async Task<Result> Create(SliderDto sliderDto)
    {
        try
        {
            var validation = await _validator.ValidateAsync(sliderDto);

            if (!validation.IsValid)
                return Result.Error(string.Join
                    (",", validation.Errors.Select(b => b.ErrorMessage)));

            var slider = _mapper.Map<Slider>(sliderDto);
            var result = await _sliderRepository.CreateAsync(slider);
            if (!result.Success)
                return Result.Error(result.Message);

            _unitOfWork.SaveChanges();

            return Result.Ok();
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }

    public async Task<Result> Delete(Guid guid)
    {
        try
        {
            var result = await _sliderRepository.RemoveAsync(guid);
            if (!result.Success)
                return Result.Error(result.Message);

            _unitOfWork.SaveChanges();

            return Result.Ok();
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }

    public async Task<Response<List<SliderDto>>> GetListByDate()
    {
        try
        {
            var Sliders = await _sliderRepository.GetListByDate();
            var SlidersDto = _mapper.Map<List<SliderDto>>(Sliders);

            return Response<List<SliderDto>>.Ok(SlidersDto);
        }
        catch (Exception ex)
        {
            return Response<List<SliderDto>>.Error(ex.Message);
        }
    }

    public async Task<Response<SliderDto>> GetById(Guid guid)
    {
        try
        {
            var slider = await _sliderRepository.GetByIdAsync(guid);
            if (slider is null)
                return Response<SliderDto>.Error("slider doesn't exist");

            var sliderDto = _mapper.Map<SliderDto>(slider);

            return Response<SliderDto>.Ok(sliderDto);
        }
        catch (Exception ex)
        {
            return Response<SliderDto>.Error(ex.Message);
        }
    }

    public async Task<Response<List<SliderDto>>> GetList()
    {
        try
        {
            var Sliders = await _sliderRepository.GetByListAsync();

            var SlidersDto = _mapper.Map<List<SliderDto>>(Sliders);

            return Response<List<SliderDto>>.Ok(SlidersDto);
        }
        catch (Exception ex)
        {
            return Response<List<SliderDto>>.Error(ex.Message);
        }
    }
}
