using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Weapsy.Blog.Data.EF.Configuration;
using Weapsy.Blog.Data.EF._new;

// ReSharper disable InconsistentNaming

namespace Weapsy.Blog.Data.EF.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddWeapsyBlogEFOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<BlogData>(c => {
                c.EFProvider = (DataProvider)Enum.Parse(
                    typeof(DataProvider),
                    configuration.GetSection("BlogData")["EFProvider"]);
            });

            services.Configure<ConnectionStrings>(configuration.GetSection("ConnectionStrings"));

            return services;
        }

        public static IServiceCollection AddWeapsyBlogEF2(this IServiceCollection services, IConfiguration configuration)
        {
            services.Scan(s => s
                .FromAssembliesOf(typeof(BlogDbContext))
                .AddClasses()
                .AsImplementedInterfaces());

            var dataProviderConfig = configuration.GetSection("BlogData")["EFProvider"];
            var connectionStringConfig = configuration.GetConnectionString("BlogConnection");

            switch (dataProviderConfig)
            {
                case "MSSQL":
                    services.AddDbContext<BlogDbContext>(options =>
                        options.UseSqlServer(connectionStringConfig));
                    services.AddTransient<_new.IDbContextFactory, MSSQLDbContextFactory>();
                    break;
                default:
                    throw new ApplicationException("The Blog EF Data Provider entry in appsettings.json is empty or the one specified has not been found.");
            }

            //var currentAssembly = typeof(ServiceCollectionExtensions).GetTypeInfo().Assembly;
            //var allFactories = currentAssembly.GetImplementationsOf<_new.IDbContextFactory>();

            //var configuredFactory = allFactories.SingleOrDefault(x => x.Provider.ToString() == dataProviderConfig);

            //if (configuredFactory == null)
            //    throw new ApplicationException("The Blog EF Data Provider entry in appsettings.json is empty or the one specified has not been found.");

            //configuredFactory.RegisterDbContextFactory(services);

            return services;
        }

        public static IServiceCollection AddWeapsyBlogEF(this IServiceCollection services, IConfiguration configuration)
        {
            services.Scan(s => s
                .FromAssembliesOf(typeof(BlogDbContext))
                .AddClasses()
                .AsImplementedInterfaces());

            var dataProviderConfig = configuration.GetSection("BlogData")["EFProvider"];
            var connectionStringConfig = configuration.GetConnectionString("BlogConnection");

            var currentAssembly = typeof(ServiceCollectionExtensions).GetTypeInfo().Assembly;
            var allDataProviders = currentAssembly.GetImplementationsOf<IDataProvider>();

            var configuredDataProvider = allDataProviders.SingleOrDefault(x => x.Provider.ToString() == dataProviderConfig);

            if (configuredDataProvider == null)
                throw new ApplicationException("The Blog EF Data Provider entry in appsettings.json is empty or the one specified has not been found.");

            configuredDataProvider.RegisterDbContext(services, connectionStringConfig);

            return services;
        }

        private static IEnumerable<T> GetImplementationsOf<T>(this Assembly assembly)
        {
            var result = new List<T>();

            var types = assembly.GetTypes()
                .Where(t => t.GetTypeInfo().IsClass && !t.GetTypeInfo().IsAbstract && typeof(T).IsAssignableFrom(t))
                .ToList();

            foreach (var type in types)
            {
                var instance = (T)Activator.CreateInstance(type);
                result.Add(instance);
            }

            return result;
        }
    }
}
