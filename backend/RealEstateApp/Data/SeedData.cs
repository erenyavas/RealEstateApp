using Microsoft.AspNetCore.Identity;
using RealEstateApp.Models;

public static class SeedData
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        
        if (!await roleManager.RoleExistsAsync("Admin"))
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
        }

        if (!await roleManager.RoleExistsAsync("User"))
        {
            await roleManager.CreateAsync(new IdentityRole("User"));
        }

        var adminUser = new ApplicationUser
        {
            UserName = "admin@admin.com",
            Email = "admin@admin.com",
            FirstName = "Admin",
            LastName = "User",
            EmailConfirmed = true
        };

        var user = await userManager.FindByEmailAsync(adminUser.Email);
        if (user == null)
        {
            var result = await userManager.CreateAsync(adminUser, "Admin@1234");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine(error.Description);
                }
            }
        }
    }
}
