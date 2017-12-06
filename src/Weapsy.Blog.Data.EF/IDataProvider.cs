using Microsoft.Extensions.DependencyInjection;
using Weapsy.Blog.Data.EF.Configuration;

namespace Weapsy.Blog.Data.EF
{
    public interface IDataProvider
    {
        DataProvider Provider { get; }
        IServiceCollection RegisterDbContext(IServiceCollection services, string connectionString);
        BlogDbContext CreateDbContext(string connectionString);
    }
}
