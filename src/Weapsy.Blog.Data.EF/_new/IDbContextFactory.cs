using Microsoft.Extensions.DependencyInjection;
using Weapsy.Blog.Data.EF.Configuration;

namespace Weapsy.Blog.Data.EF._new
{
    public interface IDbContextFactory
    {
        DataProvider Provider { get; }
        IServiceCollection RegisterDbContextFactory(IServiceCollection services, string connectionString);
        BlogDbContext CreateDbContext();
    }
}
