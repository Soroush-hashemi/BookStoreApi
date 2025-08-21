using System.Threading.Tasks;
using AutoMapper;
using BookStore.Application.DTOs.Categories;
using BookStore.Application.Interface.Categories;
using BookStore.Application.Interface.Common;
using BookStore.Domain.Entities.Category;
using BookStore.Domain.Entities.ValueObjects;
using BookStore.Shared;
using FluentValidation;

namespace BookStore.Application.Services.Categories;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<CategoryDto> _validator;
    private readonly IValidator<CategoryUpdateDto> _updatevalidator;
    private readonly ICategoryDomainService _domainService;
    private readonly IUnitOfWork _unitOfWork;

    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper,
            IValidator<CategoryDto> validator, IValidator<CategoryUpdateDto> updatevalidator,
            ICategoryDomainService domainService, IUnitOfWork unitOfWork)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
        _validator = validator;
        _updatevalidator = updatevalidator;
        _domainService = domainService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Create(CategoryDto dto)
    {
        try
        {
            var validation = await _validator.ValidateAsync(dto);

            if (!validation.IsValid)
                return Result.Error(string.Join
                    (",", validation.Errors.Select(b => b.ErrorMessage)));

            var metadataMapped = _mapper.Map<Metadata>(dto.MetadataDto);

            var category = new Category(dto.Title, dto.Slug, metadataMapped, _domainService);
            var result = await _categoryRepository.CreateAsync(category);
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

    public async Task<Result> Update(CategoryUpdateDto dto)
    {
        try
        {
            var validation = await _updatevalidator.ValidateAsync(dto);
            if (!validation.IsValid)
                return Result.Error(string.Join
                    (",", validation.Errors.Select(b => b.ErrorMessage)));

            var category = await _categoryRepository.GetByIdAsync(dto.Id);
            if (category is null)
                return Result.Error("Category doesn't exist");

            var metadataMapped = _mapper.Map<Metadata>(dto.MetadataDto);
            category.Update(dto.Title, dto.Slug, metadataMapped);

            _unitOfWork.SaveChanges();

            return Result.Ok();
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }

    public async Task<Result> Remove(Guid guid)
    {
        try
        {
            var result = await _categoryRepository.RemoveAsync(guid);
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

    public async Task<Response<CategoryDto>> GetById(Guid guid)
    {
        try
        {
            var category = await _categoryRepository.GetByIdAsync(guid);
            if (category is null)
                return Response<CategoryDto>.Error("Category doesn't exist");

            var categoryDto = _mapper.Map<CategoryDto>(category);

            return Response<CategoryDto>.Ok(categoryDto);
        }
        catch (Exception ex)
        {
            return Response<CategoryDto>.Error(ex.Message);
        }
    }

    public async Task<Response<List<CategoryDto>>> GetByList()
    {
        try
        {
            var categories = await _categoryRepository.GetByListAsync();

            var categoriesDto = _mapper.Map<List<CategoryDto>>(categories);

            return Response<List<CategoryDto>>.Ok(categoriesDto);
        }
        catch (Exception ex)
        {
            return Response<List<CategoryDto>>.Error(ex.Message);
        }
    }

    public async Task<Response<CategoryDto>> GetBySlug(string Slug)
    {
        try
        {
            var category = await _categoryRepository.GetBySlugAsync(Slug);
            if (category is null)
                return Response<CategoryDto>.Error("Category doesn't exist");

            var categoryDto = _mapper.Map<CategoryDto>(category);
            return Response<CategoryDto>.Ok(categoryDto);
        }
        catch (Exception ex)
        {
            return Response<CategoryDto>.Error(ex.Message);
        }
    }
}