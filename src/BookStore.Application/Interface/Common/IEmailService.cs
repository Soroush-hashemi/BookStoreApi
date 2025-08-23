namespace BookStore.Application.Interface.Common;

public interface IEmailService
{
    Task SendAsync(string to, string subject, string body);
}
