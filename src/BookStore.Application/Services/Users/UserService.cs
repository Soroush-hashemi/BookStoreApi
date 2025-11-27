using BookStore.Application.DTOs.UserDto;
using BookStore.Application.Interface.Users;
using BookStore.Shared;

namespace BookStore.Application.Services.Users;

public class UserService : IUserService
{
    private readonly IIdentityService _identityService;
    public UserService(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Result> AddUserToRoleAsync(Guid userId, string roleName)
    {
        try
        {
            var result = await _identityService.AddUserToRoleAsync(userId, roleName);
            if (!result.Success)
                return Result.Error($"{result.Message}");

            return result;
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }

    public async Task<Result> CreateRoleAsync(Guid UserId, string roleName)
    {
        try
        {
            var result = await _identityService.AddUserToRoleAsync(UserId, roleName);
            if (!result.Success)
                return Result.Error(result.Message);

            return result;
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }

    public async Task<Result> ChangePasswordAsync(ChangePasswordRequest request)
    {
        try
        {
            var result = await _identityService.ChangePasswordAsync(request);
            if (!result.Success)
                return Result.Error($"{result.Message}");

            return result;
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }

    public async Task<Result> ForgotPasswordAsync(string email)
    {
        try
        {
            var result = await _identityService.ForgotPasswordAsync(email);
            if (!result.Success)
                return Result.Error($"{result.Message}");

            return result;
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }

    public async Task<Result> ResetPasswordAsync(ResetPasswordRequest request)
    {
        try
        {
            var result = await _identityService.ResetPasswordAsync(request);
            if (!result.Success)
                return Result.Error($"{result.Message}");

            return result;
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }

    public async Task<IList<string>> GetUserRolesAsync(Guid userId)
    {
        try
        {
            var result = await _identityService.GetUserRolesAsync(userId);
            return result;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<Result> RemoveUserFromRoleAsync(Guid userId, string roleName)
    {
        try
        {
            var result = await _identityService.RemoveUserFromRoleAsync(userId, roleName);
            if (!result.Success)
                return Result.Error($"{result.Message}");

            return result;
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {
        try
        {
            var result = await _identityService.Login(request);
            if (!result.Success)
                return LoginResponse.Error(result.Message);

            return result;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<Result> RegisterAsync(RegisterRequest request)
    {
        try
        {
            var emailexist = await _identityService.EmailExist(request.Email);
            if (emailexist is true)
                return Result.Error($"{request.Email} is registered");

            var usernameExist = await _identityService.UsernameExist(request.Username);
            if (usernameExist is true)
                return Result.Error($"{request.Username} is registered");


            var result = await _identityService.Register(request);
            if (!result.Success)
                return Result.Error($"{result.Message}");

            var sendEmail = await _identityService.SendEmailConfirmationAsync(request.Email);
            if (!sendEmail.Success)
                return result;

            return result;
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }

    public async Task<Result> ConfirmEmailAsync(string userId, string token)
    {
        try
        {
            var result = await _identityService.ConfirmEmailAsync(userId, token);
            if (!result.Success)
                return Result.Error(result.Message);

            return result;
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }

    public async Task Logout()
    {
        try
        {
            await _identityService.Logout();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}