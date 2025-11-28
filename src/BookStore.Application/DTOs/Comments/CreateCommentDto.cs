using System;
using BookStore.Domain.Entities.Comments;

namespace BookStore.Application.DTOs.Comments;

public class CreateCommentDto
{
    public CreateCommentDto(Guid bookId, Guid userId,
        string authorName, string content)
    {
        BookId = bookId;
        UserId = userId;
        AuthorName = authorName;
        Content = content;
    }

    public Guid BookId { get; set; }
    public Guid UserId { get; set; }
    public string AuthorName { get; set; } = null!;
    public string Content { get; set; } = null!;
}