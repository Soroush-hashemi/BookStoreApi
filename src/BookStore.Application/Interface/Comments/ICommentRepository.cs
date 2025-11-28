using BookStore.Application.Interface.Common;
using BookStore.Domain.Entities.Comments;

namespace BookStore.Application.Interface;

public interface ICommentRepository : IBaseRepository<Comment>
{
    Task<List<Comment>> GetByBookAsync(Guid bookId);
    Task<List<Comment>> GetByUserAsync(Guid userId);
    Task<List<Comment>> GetByStatusAsync(CommentStatus status);
    Task<List<Comment>> GetByBookAndStatusAsync(Guid bookId, CommentStatus status);
}