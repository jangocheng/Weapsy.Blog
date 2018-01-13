using System;
using System.Collections.Generic;
using Weapsy.Blog.Domain.Blogs.Commands;
using Weapsy.Blog.Domain.Posts;
using Weapsy.Blog.Domain.Posts.Commands;

namespace Weapsy.Blog.Web
{
    public static class Factories
    {
        public static CreateBlog DefaultCreateBlogCommand(Guid blogId)
        {
            return new CreateBlog
            {
                AggregateRootId = blogId,
                Title = "My Blog",
                Theme = Constants.DefaultTheme
            };
        }

        public static CreatePost DefaultCreatePostCommand(Guid blogId)
        {
            return new CreatePost
            {
                AggregateRootId = Guid.NewGuid(),
                BlogId = blogId,
                Title = "First Post",
                Content = "Content...",
                Slug = "first-post",
                Excerpt = "Content...",
                Type = PostType.Article,
                Status = PostStatus.Published,
                Tags = new List<string> { "weapsy", "blog" }                
            };
        }
    }
}
