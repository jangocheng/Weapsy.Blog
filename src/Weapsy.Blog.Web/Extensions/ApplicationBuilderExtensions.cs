using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Weapsy.Blog.Data.Entities;
using Weapsy.Blog.Domain.Blogs.Commands;
using Weapsy.Blog.Domain.Posts;
using Weapsy.Blog.Domain.Posts.Commands;
using Weapsy.Blog.Reporting.Models;
using Weapsy.Blog.Reporting.Queries;
using Weapsy.Blog.Web.Middleware;
using Weapsy.Mediator;

namespace Weapsy.Blog.Web.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder SeedBlog(this IApplicationBuilder app, IConfiguration configuration)
        {
            var mediator = app.ApplicationServices.GetRequiredService<IMediator>();

            var defaultBlogIdConfig = configuration.GetSection(Constants.ConfigBlogSettings)[Constants.ConfigDefaultBlogId];
            var defaultBlogId = Guid.Parse(defaultBlogIdConfig);

            var query = new GetBlogSettings { BlogId = defaultBlogId };
            var blog = mediator.GetResult<GetBlogSettings, BlogSettings>(query);

            if (blog == null)
            {
                mediator.SendAndPublishAsync<CreateBlog, Domain.Blogs.Blog>(Factories.DefaultCreateBlogCommand(defaultBlogId));
                mediator.SendAndPublishAsync<CreatePost, Post>(Factories.DefaultCreatePostCommand(defaultBlogId));
            }

            return app;
        }

        public static IApplicationBuilder SeedIdentity(this IApplicationBuilder app, UserManager<UserEntity> userManager, RoleManager<RoleEntity> roleManager, IConfiguration configuration)
        {
            //var userManager = app.ApplicationServices.GetRequiredService<UserManager<UserEntity>>();
            //var roleManager = app.ApplicationServices.GetRequiredService<RoleManager<RoleEntity>>();

            if (!roleManager.RoleExistsAsync(Constants.AdministratorRoleName).GetAwaiter().GetResult())
            {
                roleManager.CreateAsync(new RoleEntity
                {
                    Id = Guid.NewGuid(),
                    Name = Constants.AdministratorRoleName
                }).GetAwaiter().GetResult();
            }

            if (userManager.Users.CountAsync().GetAwaiter().GetResult() == 0)
            {
                var defaultEmailAddress = configuration.GetSection(Constants.ConfigBlogSettings)[Constants.ConfigDefaultEmailAddress];
                var user = new UserEntity { UserName = defaultEmailAddress, Email = defaultEmailAddress };
                userManager.CreateAsync(user, Constants.DefaultPassword).GetAwaiter().GetResult();
                userManager.AddToRoleAsync(user, Constants.AdministratorRoleName).GetAwaiter().GetResult();
            }

            return app;
        }

        public static IApplicationBuilder UseTheme(this IApplicationBuilder app)
        {
            var hostingEnvironment = app.ApplicationServices.GetRequiredService<IHostingEnvironment>();

            var themesRootFolder = new DirectoryInfo(Path.Combine(hostingEnvironment.ContentRootPath, "Themes"));
            foreach (var themeFolder in themesRootFolder.GetDirectories())
            {
                var contentPath = Path.Combine(hostingEnvironment.ContentRootPath, "Themes", themeFolder.Name, "wwwroot");
                if (Directory.Exists(contentPath))
                {
                    app.UseStaticFiles(new StaticFileOptions
                    {
                        RequestPath = "/Themes/" + themeFolder.Name,
                        FileProvider = new PhysicalFileProvider(contentPath)
                    });
                }
            }

            app.UseMiddleware<ThemeMiddleware>();

            return app;
        }
    }
}