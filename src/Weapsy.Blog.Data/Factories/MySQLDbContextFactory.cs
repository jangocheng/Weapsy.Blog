using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Weapsy.Blog.Data.Configuration;

// ReSharper disable InconsistentNaming

namespace Weapsy.Blog.Data.Factories
{
    public class MySQLDbContextFactory : IDbContextFactory
    {
        public DataProvider Provider { get; } = DataProvider.MySQL;

        private readonly string _connectionString;

        public MySQLDbContextFactory()
        {          
        }

        public MySQLDbContextFactory(IOptions<ConnectionStrings> connectionStringsOptions)
        {
            _connectionString = connectionStringsOptions.Value.BlogConnection;
        }

        public IServiceCollection RegisterDbContextFactory(IServiceCollection services)
        {
            services.AddTransient<IDbContextFactory, MySQLDbContextFactory>();

            return services;
        }

        public IServiceCollection RegisterDbContext(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<BlogDbContext>(options =>
                options.UseMySQL(connectionString));

            return services;
        }

        public BlogDbContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<BlogDbContext>();
            optionsBuilder.UseMySQL(_connectionString);

            return new BlogDbContext(optionsBuilder.Options);
        }
    }
}