using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BookStore.Application.DTOs.UserDto;
using BookStore.Application.Interface.Common;
using BookStore.Application.Interface.Users;
using BookStore.Infrastructure.Identity.Data;
using BookStore.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BookStore.Infrastructure.Identity.Service;

public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IConfiguration _configuration;
    private readonly IEmailService _emailService;

    public IdentityService(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager, IConfiguration configuration
        , IEmailService emailService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
        _emailService = emailService;
    }

    public async Task<Result> AddUserToRoleAsync(Guid userId, string roleName)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
            return Result.Error("User does not exist");

        if (await _userManager.IsInRoleAsync(user, roleName))
            return Result.Error($"User is already in role '{roleName}'");

        var result = await _userManager.AddToRoleAsync(user, roleName);
        if (!result.Succeeded)
            return Result.Error(string.Join(", ", result.Errors.Select(e => e.Description)));

        return Result.Ok($"User added to role '{roleName}' successfully");
    }

    public async Task<IList<string>> GetUserRolesAsync(Guid userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user is null)
            throw new ArgumentNullException("User Does not exist");

        var result = await _userManager.GetRolesAsync(user);
        if (result is null)
            throw new ArgumentNullException("User Does not exist");

        return result;
    }

    public async Task<LoginResponse> Login(LoginRequest request)
    {
        var user = await _userManager.FindByNameAsync(request.Username);
        if (user is null)
            throw new ArgumentNullException("User Does not exist");

        if (!await _userManager.IsEmailConfirmedAsync(user))
            return LoginResponse.Error("Confirm your email first");

        var role = await _userManager.GetRolesAsync(user);
        if (role is null)
            throw new ArgumentNullException("something is wrong");

        var Result = await _signInManager.PasswordSignInAsync(request.Username,
            request.Password, request.RemeberMe, false);

        if (!Result.Succeeded)
            return LoginResponse.Error("Login was failed");

        var accessToken = GenerateToken(user, role);
        var refreshToken = GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
        await _userManager.UpdateAsync(user);

        return LoginResponse.Ok(accessToken, refreshToken);
    }

    public async Task Logout()
    {
        await _signInManager.SignOutAsync();
    }

    public async Task<Result> Register(RegisterRequest request)
    {
        var user = new ApplicationUser()
        {
            UserName = request.Username,
            Email = request.Email,
            FullName = request.FullName,
        };

        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
            return Result.Error(
                    string.Join(",", result.Errors.Select(e => e.Description)));

        await _userManager.AddToRoleAsync(user, "User");

        return Result.Ok();
    }

    public async Task<Result> ConfirmEmailAsync(string userId, string token)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return Result.Error("User Does not exist");

        var result = await _userManager.ConfirmEmailAsync(user, token);
        if (!result.Succeeded)
            return Result.Error("The token is invalid or expired");

        return Result.Ok("Email confirmed");
    }

    public async Task<Result> SendEmailConfirmationAsync(string Email)
    {
        var user = await _userManager.FindByEmailAsync(Email);
        if (user is null)
            throw new ArgumentNullException("User Does not exist");

        if (await _userManager.IsEmailConfirmedAsync(user))
            return Result.Error("Email has already been confirmed");

        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        string frontendUrl = _configuration["AppSettings:FrontendUrl"];
        string EmailConfirmationPath = _configuration["AppSettings:EmailConfirmationPath"];

        var confirmationLink =
          $"{frontendUrl}/{EmailConfirmationPath}?userId={user.Id}&token={Uri.EscapeDataString(token)}";

        await _emailService.SendAsync(
            user.Email,
            "Email Confirmation",
            $"Please click the link below to confirm your email:\n{confirmationLink}");

        return Result.Ok();
    }

    public async Task<Response<string>> RefreshTokenAsync(string refreshToken)
    {
        var user = await _userManager.Users.SingleOrDefaultAsync
            (R => R.RefreshToken == refreshToken);

        if (user is null || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            return Response<string>.Error("Invalid or expired refresh token");

        var roles = await _userManager.GetRolesAsync(user);
        var newAccessToken = GenerateToken(user, roles);
        var newRefreshToken = GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
        await _userManager.UpdateAsync(user);

        return Response<string>.Ok(newAccessToken, newRefreshToken);
    }

    public async Task<bool> UsernameExist(string Username)
    {
        var result = await _userManager.FindByNameAsync(Username);
        return result != null;
    }

    public async Task<bool> EmailExist(string Email)
    {
        var result = await _userManager.FindByEmailAsync(Email);
        return result != null;
    }

    public async Task<Result> RemoveUserFromRoleAsync(Guid userId, string roleName)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user is null)
            return Result.Error("User does not exist");

        if (!await _userManager.IsInRoleAsync(user, roleName))
            return Result.Error($"User is not in role '{roleName}'");

        var result = await _userManager.RemoveFromRoleAsync(user, userId.ToString());
        if (!result.Succeeded)
            return Result.Error("Failed to remove user from role");

        return Result.Ok($"User removed from role '{roleName}'");
    }

    public async Task<Result> ForgotPasswordAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
            return Result.Error("User does not exist");

        if (!await _userManager.IsEmailConfirmedAsync(user))
            return Result.Error("Email is not confirmed yet");

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        string frontendUrl = _configuration["AppSettings:FrontendUrl"];
        string resetPasswordPath = _configuration["AppSettings:ResetPasswordPath"];

        var resetLink =
            $"{frontendUrl}/{resetPasswordPath}?userId={user.Id}&token={Uri.EscapeDataString(token)}";

        await _emailService.SendAsync(
            user.Email,
            "Password Reset",
            $"Please reset your password by clicking the link below:\n{resetLink}");

        return Result.Ok("Password reset link has been sent to your email");
    }

    public async Task<Result> ResetPasswordAsync(ResetPasswordRequest request)
    {
        var user = await _userManager.FindByIdAsync(request.UserId.ToString());
        if (user == null)
            return Result.Error("User does not exist");

        var result = await _userManager.ResetPasswordAsync(user, request.Token, request.NewPassword);

        if (!result.Succeeded)
            return Result.Error(string.Join(", ", result.Errors.Select(e => e.Description)));

        return Result.Ok("Password has been reset successfully");
    }

    public async Task<Result> ChangePasswordAsync(ChangePasswordRequest request)
    {
        var user = await _userManager.FindByIdAsync(request.UserId.ToString());
        if (user == null)
            return Result.Error("User does not exist");

        if (!await _userManager.IsEmailConfirmedAsync(user))
            return Result.Error("Confirm your email first");

        var result = await _userManager.ChangePasswordAsync
            (user, request.CurrentPassword, request.NewPassword);

        if (!result.Succeeded)
            return Result.Error(string.Join(", ", result.Errors.Select(e => e.Description)));

        return Result.Ok("Password changed successfully");
    }

    public async Task<Result> RevokeTokenAsync(string refreshToken)
    {
        var user = await _userManager.Users.SingleAsync
            (u => u.RefreshToken == refreshToken);

        if (user is null)
            return Result.Error("Invalid refresh token");

        user.RefreshToken = null;
        user.RefreshTokenExpiryTime = DateTime.MinValue;
        await _userManager.UpdateAsync(user);

        return Result.Ok("Refresh token revoked successfully");
    }

    public async Task<Response<UserProfileDto>> GetProfileAsync(Guid userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
            return Response<UserProfileDto>.Error("User does not exist");

        var profile = new UserProfileDto(user.UserName, user.Email, user.FullName);

        return Response<UserProfileDto>.Ok(profile);
    }

    private string GenerateToken(IdentityUser user, IList<string> roles)
    {
        var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
            };

        foreach (var item in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, item));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private string GenerateRefreshToken()
    {
        byte[] bytes = new byte[64];
        var randomNumberGenerator = RandomNumberGenerator.Create();
        randomNumberGenerator.GetBytes(bytes);
        return Convert.ToBase64String(bytes);
    }
}