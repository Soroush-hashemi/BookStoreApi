namespace BookStore.Application.DTOs.UserDto;

public class UserProfileDto
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string FullName { get; set; }
    public UserProfileDto(string username, string email, string fullName)
    {
        Username = username;
        Email = email;
        FullName = fullName;
    }
}