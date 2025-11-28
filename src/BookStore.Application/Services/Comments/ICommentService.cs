using BookStore.Application.DTOs.Comments;
using BookStore.Domain.Entities.Comments;
using BookStore.Shared;

namespace BookStore.Application.Services.Comments;

public interface ICommentService
{
    Task<Result> Create(CreateCommentDto createCommentDto);
    Task<Result> Delete(Guid id);
    Task<Result> Approve(Guid id);
    Task<Result> Reject(Guid id);
    Task<Result> MarkAsSpam(Guid id);

    Task<Response<CommentDto>> GetById(Guid id);
    Task<Response<List<CommentDto>>> GetList();
    Task<Response<List<CommentDto>>> GetByBook(Guid bookId);
    Task<Response<List<CommentDto>>> GetByUser(Guid userId);
    Task<Response<List<CommentDto>>> GetByStatus(CommentStatus status);
    Task<Response<List<CommentDto>>> GetByBookAndStatus(Guid bookId, CommentStatus status);
}