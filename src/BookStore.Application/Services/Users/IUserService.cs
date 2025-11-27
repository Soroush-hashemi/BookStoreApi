using BookStore.Application.DTOs.UserDto;
using BookStore.Shared;

namespace BookStore.Application.Services.Users;

public interface IUserService
{
    Task<Result> RegisterAsync(RegisterRequest request);
    Task<LoginResponse> LoginAsync(LoginRequest request);
    Task<Result> ConfirmEmailAsync(string userId, string token);
    Task Logout();

    Task<Result> ChangePasswordAsync(ChangePasswordRequest request);
    Task<Result> ForgotPasswordAsync(string email);
    Task<Result> ResetPasswordAsync(ResetPasswordRequest request);

    Task<Result> AddUserToRoleAsync(Guid userId, string roleName);
    Task<Result> RemoveUserFromRoleAsync(Guid userId, string roleName);
    Task<IList<string>> GetUserRolesAsync(Guid userId);
    Task<Result> CreateRoleAsync(Guid UserId, string roleName);


    //Task<Result> LockUserAsync(Guid userId, TimeSpan? duration = null);
    //Task<Result> UnlockUserAsync(Guid userId);
    //Task<IList<SecurityLogDto>> GetSecurityLogsAsync(Guid userId)
}