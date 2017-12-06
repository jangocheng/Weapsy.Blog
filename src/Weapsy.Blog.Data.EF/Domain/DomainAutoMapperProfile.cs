using AutoMapper;
using Weapsy.Blog.Data.EF.Entities;
using Weapsy.Blog.Domain.Posts;

namespace Weapsy.Blog.Data.EF.Domain
{
    public class DomainAutoMapperProfile : Profile
    {
        public DomainAutoMapperProfile()
        {
            CreateMap<Blog.Domain.Blogs.Blog, BlogEntity>();
            CreateMap<BlogEntity, Blog.Domain.Blogs.Blog>().ConstructUsing(x => new Blog.Domain.Blogs.Blog());

            CreateMap<Post, PostEntity>();
            CreateMap<PostEntity, Post>().ConstructUsing(x => new Post());
        }
    }
}
