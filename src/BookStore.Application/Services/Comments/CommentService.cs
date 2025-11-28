using AutoMapper;
using BookStore.Application.DTOs.Comments;
using BookStore.Application.Interface;
using BookStore.Application.Interface.Common;
using BookStore.Domain.Entities.Comments;
using BookStore.Shared;
using FluentValidation;
using System.Linq;

namespace BookStore.Application.Services.Comments;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateCommentDto> _createValidator;

    public CommentService(ICommentRepository commentRepository,
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IValidator<CreateCommentDto> createValidator)
    {
        _commentRepository = commentRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _createValidator = createValidator;
    }

    public async Task<Result> Create(CreateCommentDto createCommentDto)
    {
        try
        {
            var validation = await _createValidator.ValidateAsync(createCommentDto);
            if (!validation.IsValid)
                return Result.Error(string.Join(",", validation.Errors.Select(e => e.ErrorMessage)));

            var comment = new Comment( createCommentDto.AuthorName,
                createCommentDto.Content,
                createCommentDto.BookId,
                createCommentDto.UserId);

            var result = await _commentRepository.CreateAsync(comment);
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

    public async Task<Result> Delete(Guid id)
    {
        try
        {
            var result = await _commentRepository.RemoveAsync(id);
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

    public async Task<Result> Approve(Guid id)
    {
        try
        {
            var comment = await _commentRepository.GetByIdAsync(id);
            if (comment is null)
                return Result.Error("Comment doesn't exist");

            comment.Approve();

            _unitOfWork.SaveChanges();

            return Result.Ok();
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }

    public async Task<Result> Reject(Guid id)
    {
        try
        {
            var comment = await _commentRepository.GetByIdAsync(id);
            if (comment is null)
                return Result.Error("Comment doesn't exist");

            comment.Reject();

            _unitOfWork.SaveChanges();

            return Result.Ok();
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }

    public async Task<Result> MarkAsSpam(Guid id)
    {
        try
        {
            var comment = await _commentRepository.GetByIdAsync(id);
            if (comment is null)
                return Result.Error("Comment doesn't exist");

            comment.MarkAsSpam();

            _unitOfWork.SaveChanges();

            return Result.Ok();
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }

    public async Task<Response<CommentDto>> GetById(Guid id)
    {
        try
        {
            var comment = await _commentRepository.GetByIdAsync(id);
            if (comment is null)
                return Response<CommentDto>.Error("comment doesn't exist");

            var dto = _mapper.Map<CommentDto>(comment);

            return Response<CommentDto>.Ok(dto);
        }
        catch (Exception ex)
        {
            return Response<CommentDto>.Error(ex.Message);
        }
    }

    public async Task<Response<List<CommentDto>>> GetList()
    {
        try
        {
            var comments = await _commentRepository.GetByListAsync();
            var dtos = _mapper.Map<List<CommentDto>>(comments);

            return Response<List<CommentDto>>.Ok(dtos);
        }
        catch (Exception ex)
        {
            return Response<List<CommentDto>>.Error(ex.Message);
        }
    }

    public async Task<Response<List<CommentDto>>> GetByBook(Guid bookId)
    {
        try
        {
            var comments = await _commentRepository.GetByBookAsync(bookId);
            var dtos = _mapper.Map<List<CommentDto>>(comments);

            return Response<List<CommentDto>>.Ok(dtos);
        }
        catch (Exception ex)
        {
            return Response<List<CommentDto>>.Error(ex.Message);
        }
    }

    public async Task<Response<List<CommentDto>>> GetByUser(Guid userId)
    {
        try
        {
            var comments = await _commentRepository.GetByUserAsync(userId);
            var dtos = _mapper.Map<List<CommentDto>>(comments);

            return Response<List<CommentDto>>.Ok(dtos);
        }
        catch (Exception ex)
        {
            return Response<List<CommentDto>>.Error(ex.Message);
        }
    }

    public async Task<Response<List<CommentDto>>> GetByStatus(CommentStatus status)
    {
        try
        {
            var comments = await _commentRepository.GetByStatusAsync(status);
            var dtos = _mapper.Map<List<CommentDto>>(comments);

            return Response<List<CommentDto>>.Ok(dtos);
        }
        catch (Exception ex)
        {
            return Response<List<CommentDto>>.Error(ex.Message);
        }
    }

    public async Task<Response<List<CommentDto>>> GetByBookAndStatus(Guid bookId, CommentStatus status)
    {
        try
        {
            var comments = await _commentRepository.GetByBookAndStatusAsync(bookId, status);
            var dtos = _mapper.Map<List<CommentDto>>(comments);

            return Response<List<CommentDto>>.Ok(dtos);
        }
        catch (Exception ex)
        {
            return Response<List<CommentDto>>.Error(ex.Message);
        }
    }
}