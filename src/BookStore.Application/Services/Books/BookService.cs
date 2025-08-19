using AutoMapper;
using BookStore.Application.DTOs.Books;
using BookStore.Application.Interface;
using BookStore.Application.Interface.Common;
using BookStore.Domain.Entities.Book;
using BookStore.Domain.Entities.Books;
using BookStore.Domain.Entities.ValueObjects;
using BookStore.Shared;
using FluentValidation;

namespace BookStore.Application.Services.Books;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<BookDto> _validator;
    private readonly IValidator<BookUpdateDto> _updatevalidator;
    private readonly IBookDomainService _bookDomainService;
    private readonly IUnitOfWork _unitOfWork;
    public BookService(IBookRepository bookRepository, IMapper mapper,
        IValidator<BookDto> validator, IValidator<BookUpdateDto> updatevalidator,
        IBookDomainService bookDomainService, IUnitOfWork unitOfWork)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
        _validator = validator;
        _updatevalidator = updatevalidator;
        _bookDomainService = bookDomainService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Create(BookDto dto)
    {
        try
        {
            var validation = await _validator.ValidateAsync(dto);

            if (!validation.IsValid)
                return Result.Error(string.Join
                    (",", validation.Errors.Select(b => b.ErrorMessage)));

            var metadataMapped = _mapper.Map<Metadata>(dto.MetadataDto);

            var book = new Book(dto.Title, dto.Slug, dto.PDF, dto.Price,
                dto.Image, dto.Description, dto.Author, dto.CategoryId, metadataMapped, _bookDomainService);

            var result = await _bookRepository.CreateAsync(book);

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

    public async Task<Result> Update(BookUpdateDto dto)
    {
        try
        {
            var validation = await _updatevalidator.ValidateAsync(dto);
            if (!validation.IsValid)
                return Result.Error(string.Join
                    (",", validation.Errors.Select(b => b.ErrorMessage)));

            var book = await _bookRepository.GetByIdAsync(dto.Id);

            if (book is null)
                return Result.Error("Book doesn't exist");

            var metadataMapped = _mapper.Map<Metadata>(dto.MetadataDto);

            book.Update(dto.Title, dto.PDF, dto.Price, metadataMapped,
                dto.Image, dto.Description, dto.Author, dto.CategoryId);

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
            var result = await _bookRepository.RemoveAsync(guid);

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

    public async Task<Result> UpdatePrice(Guid guid, int price)
    {
        try
        {
            var book = await _bookRepository.GetByIdAsync(guid);
            if (book is null)
                return Result.Error("Book doesn't exist");

            book.UpdatePrice(price);

            _unitOfWork.SaveChanges();

            return Result.Ok();
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }

    public async Task<Response<List<BookDto>>> GetByAuthor(string Author)
    {
        try
        {
            var books = await _bookRepository.GetByAuthorAsync(Author);
            if (books is null)
                return Response<List<BookDto>>.Error();

            var bookDtos = _mapper.Map<List<BookDto>>(books);
            return Response<List<BookDto>>.Ok(bookDtos);
        }
        catch (Exception ex)
        {
            return Response<List<BookDto>>.Error(ex.Message);
        }
    }

    public async Task<Response<List<BookDto>>> GetByCategory(string categoryslug)
    {
        try
        {
            var books = await _bookRepository.GetByCategoryAsync(categoryslug);
            if (books is null)
                return Response<List<BookDto>>.Error();

            var bookDtos = _mapper.Map<List<BookDto>>(books);

            return Response<List<BookDto>>.Ok(bookDtos);
        }
        catch (Exception ex)
        {
            return Response<List<BookDto>>.Error(ex.Message);
        }
    }

    public async Task<Response<BookDto>> GetBySlug(string Slug)
    {
        try
        {
            var book = await _bookRepository.GetBySlugAsync(Slug);
            var bookDto = _mapper.Map<BookDto>(book);

            return Response<BookDto>.Ok(bookDto);
        }
        catch (Exception ex)
        {
            return Response<BookDto>.Error(ex.Message);
        }
    }

    public async Task<Response<BookDto>> GetById(Guid guid)
    {
        try
        {
            var book = await _bookRepository.GetByIdAsync(guid);
            if (book is null)
                return Response<BookDto>.Error("book doesn't Exist");

            var bookDto = _mapper.Map<BookDto>(book);

            return Response<BookDto>.Ok(bookDto);
        }
        catch (Exception ex)
        {
            return Response<BookDto>.Error(ex.Message);
        }
    }

    public async Task<Response<List<BookDto>>> GetByList()
    {
        try
        {
            var books = await _bookRepository.GetByListAsync();
            if (books is null)
                return Response<List<BookDto>>.Error();

            var bookDtos = _mapper.Map<List<BookDto>>(books);

            return Response<List<BookDto>>.Ok(bookDtos);
        }
        catch (Exception ex)
        {
            return Response<List<BookDto>>.Error(ex.Message);
        }

    }
}