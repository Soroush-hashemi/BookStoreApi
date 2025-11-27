namespace BookStore.Application.DTOs.UserDto;

public class LoginResponse
{
    public LoginResponse(bool success, string accessToken)
    {
        Success = success;
        AccessToken = accessToken;
    }

    public LoginResponse(bool success, string message, string accessToken)
    {
        Success = success;
        Message = message;
        AccessToken = accessToken;
    }

    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;

    public static LoginResponse Error(string message)
            => new LoginResponse(false, message);

    public static LoginResponse Ok(string accessToken, string refreshToken)
            => new LoginResponse(true, accessToken , refreshToken);
}