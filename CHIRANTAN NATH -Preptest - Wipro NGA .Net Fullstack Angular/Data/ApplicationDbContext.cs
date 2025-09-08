namespace SecureLoginApp.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options), IdentityDbContext<ApplicationUser>
    {
    }
}