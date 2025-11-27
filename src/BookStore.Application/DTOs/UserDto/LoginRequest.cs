using System;

namespace BookStore.Application.DTOs.UserDto;

public class LoginRequest
{
    public LoginRequest(string username, bool remeberMe, string password)
    {
        Username = username;
        Password = password;
        RemeberMe = remeberMe;
    }

    public string Username { get; set; }
    public string Password { get; set; }
    public bool RemeberMe { get; set; }

}