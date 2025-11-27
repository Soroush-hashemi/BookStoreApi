using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace BookStore.Infrastructure.Identity.Data;

public class SeedRoles
{
    public static async Task SeedAsync(IServiceProvider service)
    {
        var roleManager = service.GetRequiredService<RoleManager<ApplicationRole>>();
        var userManager = service.GetRequiredService<UserManager<ApplicationUser>>();

        foreach (var role in ApplicationRole.DefaultRoles)
        {
            if (!await roleManager.RoleExistsAsync(role.Name))
            {
                await roleManager.CreateAsync(role);
            }
        }

        string adminEmail = "admin@example.com";
        var adminUser = await userManager.FindByEmailAsync(adminEmail);

        if (adminUser == null)
        {
            adminUser = new ApplicationUser
            {
                UserName = "admin",
                Email = adminEmail,
                FullName = "book store",
                EmailConfirmed = true
            };

            var createAdmin = await userManager.CreateAsync(adminUser, "Admin@123");
            if (createAdmin.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }

        }
    }
}