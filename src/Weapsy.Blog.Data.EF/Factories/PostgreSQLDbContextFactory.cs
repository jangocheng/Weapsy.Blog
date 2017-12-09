using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Weapsy.Blog.Data.EF.Configuration;

// ReSharper disable InconsistentNaming

namespace Weapsy.Blog.Data.EF.Factories
{
    public class PostgreSQLDbContextFactory : IDbContextFactory
    {
        public DataProvider Provider { get; } = DataProvider.PostgreSQL;

        private readonly string _connectionString;

        public PostgreSQLDbContextFactory()
        {
            
        }

        public PostgreSQLDbContextFactory(IOptions<ConnectionStrings> connectionStringsOptions)
        {
            _connectionString = connectionStringsOptions.Value.BlogConnection;
        }

        public IServiceCollection RegisterDbContextFactory(IServiceCollection services)
        {
            services.AddTransient<IDbContextFactory, PostgreSQLDbContextFactory>();

            return services;
        }

        public IServiceCollection RegisterDbContext(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<BlogDbContext>(options =>
                options.UseNpgsql(connectionString));

            return services;
        }

        public BlogDbContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<BlogDbContext>();
            optionsBuilder.UseNpgsql(_connectionString);

            return new BlogDbContext(optionsBuilder.Options);
        }
    }
}