using System;
using BookStore.Domain.Entities.Comments;

namespace BookStore.Application.DTOs.Comments;

public class CommentDto
{
    public CommentDto(Guid id, Guid bookId, Guid userId, 
    string authorName, string content, CommentStatus status, DateTime createdAt)
    {
        Id = id;
        BookId = bookId;
        UserId = userId;
        AuthorName = authorName;
        Content = content;
        Status = status;
        CreatedAt = createdAt;
    }

    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public Guid UserId { get; set; }
    public string AuthorName { get; set; } = null!;
    public string Content { get; set; } = null!;
    public CommentStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
}