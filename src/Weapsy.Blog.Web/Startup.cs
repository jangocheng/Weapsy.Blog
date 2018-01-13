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
using Weapsy.Mediator.Dependencies;
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
            services.AddWeapsyMediatorWithEventStore(Configuration);
            services.AddWeapsyBlogWithEF(Configuration);
            services.AddWeapsyBlogAutoMapper();
            services.AddWeapsyBlogThemes();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
            IHostingEnvironment env,
            BlogDbContext blogDbContext,
            EventStoreDbContext eventStoreDbContext,
            UserManager<UserEntity> userManager, 
            RoleManager<RoleEntity> roleManager,
            IResolver resolver)
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
                    name: "area",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            blogDbContext.Database.Migrate();
            eventStoreDbContext.Database.Migrate();

            //app.EnsureMediatorDbCreated();
            //app.EnsureBlogDbCreated();
            app.SeedBlog(Configuration);
            app.SeedIdentity(userManager, roleManager, Configuration);
            app.UseTheme();
        }
    }
}
