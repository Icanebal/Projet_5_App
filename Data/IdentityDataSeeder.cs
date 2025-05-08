using Microsoft.AspNetCore.Identity;
using Projet_5_App.Models.Identity;

namespace Projet_5_App.Data
{
    public static class IdentityDataSeeder
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            var adminUser = await userManager.FindByEmailAsync("admin@expressvoitures.fr");

            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = "admin@expressvoitures.fr",
                    Email = "admin@expressvoitures.fr",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, "Admin123!");

                if (!result.Succeeded)
                {
                    throw new Exception("Failed to create admin user: " + string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }

            if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
}
