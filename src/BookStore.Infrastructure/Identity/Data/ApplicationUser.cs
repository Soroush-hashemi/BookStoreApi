using Microsoft.AspNetCore.Identity;

namespace BookStore.Infrastructure.Identity.Data;

public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; } = string.Empty;
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
}