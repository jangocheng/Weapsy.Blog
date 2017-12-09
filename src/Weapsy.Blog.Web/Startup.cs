using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Weapsy.Blog.Data.EF;
using Weapsy.Blog.Web.Data;
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

            services.AddIdentity(Configuration);

            services.AddMvc();

            services.AddWeapsyMediatorWithEF(Configuration);
            services.AddWeapsyBlogWithEF(Configuration);
            services.AddWeapsyBlogAutoMapper();
            services.AddWeapsyBlogThemes();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
            IHostingEnvironment env,
            ApplicationDbContext applicationDbContext,
            BlogDbContext blogDbContext,
            MediatorDbContext mediatorDbContext)
        {
            applicationDbContext.Database.Migrate();
            blogDbContext.Database.Migrate();
            mediatorDbContext.Database.Migrate();

            //app.EnsureMediatorDbCreated();
            //app.EnsureBlogDbCreated();
            app.EnsureDefaultBlogCreated();

            app.UseTheme();

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
        }
    }
}
