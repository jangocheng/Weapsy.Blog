using System;
using System.Linq;
using Microsoft.Extensions.Options;
using Weapsy.Blog.Data.EF.Configuration;
using Weapsy.Mediator.Dependencies;

namespace Weapsy.Blog.Data.EF
{
    public class DbContextFactory : IDbContextFactory
    {
        private BlogData BlogData { get; }
        private ConnectionStrings ConnectionStrings { get; }
        private readonly IResolver _resolver;

        public DbContextFactory(IOptions<BlogData> blogDataOptions,
            IOptions<ConnectionStrings> connectionStringsOptions,
            IResolver resolver)
        {
            BlogData = blogDataOptions.Value;
            ConnectionStrings = connectionStringsOptions.Value;
            _resolver = resolver;
        }

        public BlogDbContext CreateDbContext()
        {
            var dataProvider = _resolver.ResolveAll<IDataProvider>().SingleOrDefault(x => x.Provider == BlogData.EFProvider);

            if (dataProvider == null)
                throw new Exception("The Data Provider entry in appsettings.json is empty or the one specified has not been found.");

            return dataProvider.CreateDbContext(ConnectionStrings.BlogConnection);
        }
    }
}
