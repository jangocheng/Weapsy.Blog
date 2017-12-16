using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Weapsy.Blog.Data
{
    public class MigrationsDbContextFactory : IDesignTimeDbContextFactory<BlogDbContext>
    {
        public BlogDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<BlogDbContext>();
            builder.UseSqlServer("UsedForMigrationsOnlyUntilClassLibraryBugIsFixed");

            return new BlogDbContext(builder.Options);
        }
    }
}