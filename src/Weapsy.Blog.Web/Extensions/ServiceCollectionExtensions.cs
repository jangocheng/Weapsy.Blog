using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Weapsy.Blog.Data;
using Weapsy.Blog.Data.Domain;
using Weapsy.Blog.Data.Entities;
using Weapsy.Blog.Data.Extensions;
using Weapsy.Blog.Data.Reporting;
using Weapsy.Blog.Domain.Blogs.Commands;
using Weapsy.Blog.Reporting.Queries;
using Weapsy.Blog.Web.Configuration;
using Weapsy.Mediator.EventStore.EF.SqlServer;
using Weapsy.Mediator.Extensions;

// ReSharper disable InconsistentNaming

namespace Weapsy.Blog.Web.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddWeapsyBlog(this IServiceCollection services)
        {
            services.Scan(s => s
                .FromAssembliesOf(typeof(CreateBlog), typeof(GetBlogSettings))
                .AddClasses()
                .AsImplementedInterfaces());

            return services;
        }

        public static IServiceCollection AddWeapsyBlogOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<BlogSettings>(configuration.GetSection(Constants.ConfigBlogSettings));

            return services;
        }

        public static IServiceCollection AddWeapsyBlogWithEF(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddWeapsyBlog();
            services.AddWeapsyBlogOptions(configuration);
            services.AddWeapsyBlogEFOptions(configuration);
            services.AddWeapsyBlogEF(configuration);

            return services;
        }

        public static IServiceCollection AddWeapsyMediatorWithEventStore(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddWeapsyMediator(typeof(CreateBlog), typeof(GetBlogSettings));
            services.AddWeapsyMediatorEventStore(configuration);

            return services;
        }

        public static IServiceCollection AddWeapsyBlogAutoMapper(this IServiceCollection services)
        {
            var autoMapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DomainAutoMapperProfile());
                cfg.AddProfile(new ReportingAutoMapperProfile());
            });

            services.AddSingleton(sp => autoMapperConfig.CreateMapper());

            return services;
        }

        public static IServiceCollection AddWeapsyBlogThemes(this IServiceCollection services)
        {
            services.Configure<RazorViewEngineOptions>(options => {
                options.ViewLocationExpanders.Add(new ViewLocationExpander());
            });

            return services;
        }

        public static IServiceCollection AddWeapsyBlogIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BlogDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString(Data.Constants.ConfigBlogConnection)));

            services.AddIdentity<UserEntity, RoleEntity>()
                .AddEntityFrameworkStores<BlogDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }
    }
}
