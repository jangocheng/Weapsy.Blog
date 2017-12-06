using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Weapsy.Blog.Domain.Blogs.Commands;
using Weapsy.Blog.Reporting.Blogs;
using Weapsy.Blog.Reporting.Blogs.Queries;
using Weapsy.Mediator;

namespace Weapsy.Blog.Web.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder EnsureDefaultBlogCreated(this IApplicationBuilder app)
        {
            var mediator = app.ApplicationServices.GetRequiredService<IMediator>();

            var blogId = new Guid(Constants.DefaultBlogId);
            var query = new GetBlog { BlogId = blogId };
            var blog = mediator.GetResult<GetBlog, BlogViewModel>(query);

            if (blog == null)
            {
                var command = new CreateBlog { AggregateRootId = blogId, Title = Constants.DefaultBlogTitle };
                mediator.SendAndPublishAsync<CreateBlog, Domain.Blogs.Blog>(command);
            }

            return app;
        }
    }
}