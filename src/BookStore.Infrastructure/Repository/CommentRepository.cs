using BookStore.Application.Interface;
using BookStore.Domain.Entities.Comments;
using BookStore.Infrastructure.Persistence;
using BookStore.Infrastructure.Repository.Common;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.Repository;

public class CommentRepository : BaseRepository<Comment>, ICommentRepository
{
    private readonly BookStoreDbContext _dbContext;

    public CommentRepository(BookStoreDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Comment>> GetByBookAsync(Guid bookId)
    {
        return await _dbContext.Comments
            .Where(c => c.BookId == bookId)
            .ToListAsync();
    }
    public async Task<List<Comment>> GetByUserAsync(Guid userId)
    {
        return await _dbContext.Comments
            .Where(c => c.UserId == userId)
            .ToListAsync();
    }

    public async Task<List<Comment>> GetByStatusAsync(CommentStatus status)
    {
        return await _dbContext.Comments
            .Where(c => c.Status == status)
            .ToListAsync();
    }
    public async Task<List<Comment>> GetByBookAndStatusAsync(Guid bookId, CommentStatus status)
    {
        return await _dbContext.Comments
            .Where(c => c.BookId == bookId && c.Status == status)
            .ToListAsync();
    }

    public async Task<bool> HasUserCommentedOnBookAsync(Guid userId, Guid bookId)
    {
        return await _dbContext.Comments
            .AnyAsync(c => c.UserId == userId && c.BookId == bookId);
    }
}