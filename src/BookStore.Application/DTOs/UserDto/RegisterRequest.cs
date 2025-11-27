using System;

namespace BookStore.Application.DTOs.UserDto;

public class RegisterRequest
{
    public RegisterRequest(string email, string password,
         string username, string fullName)
    {
        Email = email;
        Password = password;
        Username = username;
        FullName = fullName;
    }

    public string Email { get; set; }
    public string Password { get; set; }
    public string Username { get; set; }
    public string FullName { get; set; }
}
