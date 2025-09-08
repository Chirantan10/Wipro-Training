public static class DataSeeder
{
    public static async Task SeedData(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        // Create roles if they don't exist
        if (!await roleManager.RoleExistsAsync("Admin"))
            await roleManager.CreateAsync(new IdentityRole("Admin"));
        if (!await roleManager.RoleExistsAsync("User"))
            await roleManager.CreateAsync(new IdentityRole("User"));

        // Create default admin
        var adminUser = await userManager.FindByNameAsync("admin");
        if (adminUser == null)
        {
            adminUser = new ApplicationUser { UserName = "admin", Email = "admin@example.com" };
            var result = await userManager.CreateAsync(adminUser, "Admin@123");
            if (result.Succeeded)
                await userManager.AddToRoleAsync(adminUser, "Admin");
        }

        // Create default user
        var normalUser = await userManager.FindByNameAsync("user1");
        if (normalUser == null)
        {
            normalUser = new ApplicationUser { UserName = "user1", Email = "user1@example.com" };
            var result = await userManager.CreateAsync(normalUser, "User@123");
            if (result.Succeeded)
                await userManager.AddToRoleAsync(normalUser, "User");
        }
    }
}
