using BookStore.Domain.Common;
using BookStore.Domain.Exceptions;

namespace BookStore.Domain.Entities.Comments;

public class Comment : BaseEntity
{
    public string Content { get; private set; }
    public string AuthorName { get; private set; }
    public CommentStatus Status { get; private set; }
    public Guid BookId { get; private set; }
    public Guid UserId { get; private set; }

    public Comment(string authorName, string content,
        Guid bookId, Guid userId)
    {
        Validate(authorName, content);

        BookId = bookId;
        UserId = userId;
        AuthorName = authorName;
        Content = content;
        Status = CommentStatus.Pending;
    }
    public void SetStatus(CommentStatus status)
    {
        Status = status;
    }

    public void Approve() => SetStatus(CommentStatus.Approved);
    public void Reject() => SetStatus(CommentStatus.Rejected);
    public void MarkAsSpam() => SetStatus(CommentStatus.Spam);

    private void Validate(string authorName, string content)
    {
        if (string.IsNullOrEmpty(authorName))
            throw new NullPropertyException("authorName is null");

        if (string.IsNullOrEmpty(content))
            throw new NullPropertyException("content is null");
    }
}
