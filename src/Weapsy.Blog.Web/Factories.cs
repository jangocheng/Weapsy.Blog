using System;
using System.Collections.Generic;
using Weapsy.Blog.Domain.Blogs.Commands;
using Weapsy.Blog.Domain.Posts;
using Weapsy.Blog.Domain.Posts.Commands;

namespace Weapsy.Blog.Web
{
    public static class Factories
    {
        public static CreateBlog DefaultCreateBlogCommand()
        {
            return new CreateBlog
            {
                AggregateRootId = Constants.DefaultBlogId,
                Title = "My Blog",
                Theme = Constants.DefaultTheme
            };
        }

        public static CreatePost DefaultCreatePostCommand()
        {
            return new CreatePost
            {
                AggregateRootId = Guid.NewGuid(),
                BlogId = Constants.DefaultBlogId,
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
