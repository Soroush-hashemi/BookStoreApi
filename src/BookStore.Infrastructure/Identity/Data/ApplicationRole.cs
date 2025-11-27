using Microsoft.AspNetCore.Identity;

namespace BookStore.Infrastructure.Identity.Data;

public class ApplicationRole : IdentityRole
{
    public ApplicationRole() : base() { }

    public ApplicationRole(string roleName) : base(roleName)
    {

    }

    public static List<ApplicationRole> DefaultRoles => new List<ApplicationRole>
    {
        new ApplicationRole("Admin"),
        new ApplicationRole("Seller"),
        new ApplicationRole("User")
    };
}