using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Weapsy.Blog.Data.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder EnsureBlogDbCreated(this IApplicationBuilder app)
        {
            var dbContext = app.ApplicationServices.GetRequiredService<BlogDbContext>();

            dbContext.Database.Migrate();

            return app;
        }
    }
}
