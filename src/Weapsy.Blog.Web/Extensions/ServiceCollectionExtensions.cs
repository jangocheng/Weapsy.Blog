using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Weapsy.Blog.Data.EF.Domain;
using Weapsy.Blog.Data.EF.Extensions;
using Weapsy.Blog.Data.EF.Reporting;
using Weapsy.Blog.Domain.Blogs.Commands;
using Weapsy.Blog.Reporting.Blogs.Queries;
using Weapsy.Blog.Web.Data;
using Weapsy.Blog.Web.Models;
using Weapsy.Mediator.EventStore.EF.Extensions;
using Weapsy.Mediator.Extensions;

// ReSharper disable InconsistentNaming

namespace Weapsy.Blog.Web.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddWeapsyBlog(this IServiceCollection services)
        {
            services.Scan(s => s
                .FromAssembliesOf(typeof(CreateBlog), typeof(GetBlog))
                .AddClasses()
                .AsImplementedInterfaces());

            return services;
        }

        public static IServiceCollection AddWeapsyBlogWithEF(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddWeapsyBlog();
            services.AddWeapsyBlogEFOptions(configuration);
            services.AddWeapsyBlogEF(configuration);

            return services;
        }

        public static IServiceCollection AddWeapsyMediatorWithEF(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddWeapsyMediator(typeof(CreateBlog), typeof(GetBlog));
            services.AddWeapsyMediatorEFOptions(configuration);
            services.AddWeapsyMediatorEF(configuration);

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

        public static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("IdentityConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }
    }
}
