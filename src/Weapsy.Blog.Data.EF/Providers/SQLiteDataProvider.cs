using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Weapsy.Blog.Data.EF.Configuration;

// ReSharper disable InconsistentNaming

namespace Weapsy.Blog.Data.EF.Providers
{
    public class SQLiteDataProvider : IDataProvider
    {
        public DataProvider Provider { get; } = DataProvider.SQLite;

        public IServiceCollection RegisterDbContext(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<BlogDbContext>(options =>
                options.UseSqlite(connectionString));

            return services;
        }

        public BlogDbContext CreateDbContext(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BlogDbContext>();
            optionsBuilder.UseSqlite(connectionString);

            return new BlogDbContext(optionsBuilder.Options);
        }
    }
}