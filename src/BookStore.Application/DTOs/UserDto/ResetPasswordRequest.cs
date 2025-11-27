namespace BookStore.Application.DTOs.UserDto;

public class ResetPasswordRequest
{
    public Guid UserId { get; set; }
    public string Token { get; set; }
    public string NewPassword { get; set; }

    public ResetPasswordRequest(Guid userId, string token,
            string newPassword)
    {
        UserId = userId;
        Token = token;
        NewPassword = newPassword;
    }
}