using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Weapsy.Blog.Data.Configuration;

// ReSharper disable InconsistentNaming

namespace Weapsy.Blog.Data.Factories
{
    public class SQLiteDbContextFactory : IDbContextFactory
    {
        public DataProvider Provider { get; } = DataProvider.SQLite;

        private readonly string _connectionString;

        public SQLiteDbContextFactory()
        {            
        }

        public SQLiteDbContextFactory(IOptions<ConnectionStrings> connectionStringsOptions)
        {
            _connectionString = connectionStringsOptions.Value.BlogConnection;
        }

        public IServiceCollection RegisterDbContextFactory(IServiceCollection services)
        {
            services.AddTransient<IDbContextFactory, SQLiteDbContextFactory>();

            return services;
        }

        public IServiceCollection RegisterDbContext(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<BlogDbContext>(options =>
                options.UseSqlite(connectionString));

            return services;
        }

        public BlogDbContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<BlogDbContext>();
            optionsBuilder.UseSqlite(_connectionString);

            return new BlogDbContext(optionsBuilder.Options);
        }
    }
}