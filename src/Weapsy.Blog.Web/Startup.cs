using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Weapsy.Blog.Data;
using Weapsy.Blog.Data.Entities;
using Weapsy.Blog.Web.Extensions;
using Weapsy.Blog.Web.Services;
using Weapsy.Mediator.EventStore.EF;

namespace Weapsy.Blog.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();

            services.AddMvc();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddWeapsyBlogIdentity(Configuration);
            services.AddWeapsyMediatorWithEF(Configuration);
            services.AddWeapsyBlogWithEF(Configuration);
            services.AddWeapsyBlogAutoMapper();
            services.AddWeapsyBlogThemes();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
            IHostingEnvironment env,
            BlogDbContext blogDbContext,
            MediatorDbContext mediatorDbContext,
            UserManager<UserEntity> userManager, 
            RoleManager<RoleEntity> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            blogDbContext.Database.Migrate();
            mediatorDbContext.Database.Migrate();

            //app.EnsureMediatorDbCreated();
            //app.EnsureBlogDbCreated();
            app.EnsureDefaultBlogCreated();
            app.EnsureDefaultUserCreated(userManager, roleManager);
            app.UseTheme();
        }
    }
}
