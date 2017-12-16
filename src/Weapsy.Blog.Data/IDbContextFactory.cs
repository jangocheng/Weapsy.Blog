using Microsoft.Extensions.DependencyInjection;
using Weapsy.Blog.Data.Configuration;

namespace Weapsy.Blog.Data
{
    public interface IDbContextFactory
    {
        DataProvider Provider { get; }
        IServiceCollection RegisterDbContextFactory(IServiceCollection services);
        IServiceCollection RegisterDbContext(IServiceCollection services, string connectionString);
        BlogDbContext CreateDbContext();
    }
}
