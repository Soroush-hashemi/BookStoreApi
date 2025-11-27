using BookStore.Application.DTOs.UserDto;
using BookStore.Shared;

namespace BookStore.Application.Interface.Users;

public interface IIdentityService
{
    Task<Result> Register(RegisterRequest request);
    Task<Result> SendEmailConfirmationAsync(string Email);
    Task<Result> ConfirmEmailAsync(string userId, string token);
    Task<LoginResponse> Login(LoginRequest request);
    Task Logout();
    Task<bool> EmailExist(string Email);
    Task<bool> UsernameExist(string Username);
    Task<Result> ChangePasswordAsync(ChangePasswordRequest request);
    Task<Response<UserProfileDto>> GetProfileAsync(Guid userId);
    Task<Result> AddUserToRoleAsync(Guid userId, string roleName);
    Task<Result> RemoveUserFromRoleAsync(Guid userId, string roleName);
    Task<IList<string>> GetUserRolesAsync(Guid userId);

    Task<Result> ForgotPasswordAsync(string email);
    Task<Result> ResetPasswordAsync(ResetPasswordRequest request);

    Task<Response<string>> RefreshTokenAsync(string refreshToken);
    Task<Result> RevokeTokenAsync(string refreshToken);

    //Task<Response<CartDto>> GetUserCartAsync(Guid userId);
    //Task<Response<List<OrderDto>>> GetUserOrdersAsync(Guid userId);

    //Task<IList<SecurityLogDto>> GetSecurityLogsAsync(Guid userId);
    //Task<Result> LockUserAsync(Guid userId, TimeSpan? duration = null);
    //Task<Result> UnlockUserAsync(Guid userId);
}