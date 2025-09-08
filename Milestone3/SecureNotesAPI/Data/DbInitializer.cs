using Microsoft.EntityFrameworkCore;
using SecureNotesAPI.Data;

namespace SecureNotesAPI.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.Migrate();
        }
    }
}