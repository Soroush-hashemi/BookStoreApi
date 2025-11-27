using System;

namespace BookStore.Application.DTOs.UserDto;

public class ChangePasswordRequest
{
    public Guid UserId { get; set; }
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
    public ChangePasswordRequest(Guid userId, string newPassword, string currentPassword)
    {
        UserId = userId;
        NewPassword = newPassword;
        CurrentPassword = currentPassword;
    }
}