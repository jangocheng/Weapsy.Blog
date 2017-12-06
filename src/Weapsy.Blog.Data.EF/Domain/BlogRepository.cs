using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Weapsy.Blog.Data.EF.Entities;
using Weapsy.Blog.Domain.Blogs;

namespace Weapsy.Blog.Data.EF.Domain
{
    public class BlogRepository : IBlogRepository
    {
        private readonly _new.IDbContextFactory _dbContextFactory;
        private readonly IMapper _mapper;

        public BlogRepository(_new.IDbContextFactory dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        public async Task<Blog.Domain.Blogs.Blog> GetByIdAsync(Guid blogId)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                var blogEntity = await dbContext.Blogs.FirstOrDefaultAsync(x => x.Id == blogId);
                return blogEntity != null ? _mapper.Map<Blog.Domain.Blogs.Blog>(blogEntity) : null;
            }
        }

        public async Task<Guid> GetBlogIdByTitleAsync(string title)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                return await dbContext.Blogs.Where(x => x.Title == title).Select(x => x.Id).FirstOrDefaultAsync();
            }
        }

        public async Task CreateAsync(Blog.Domain.Blogs.Blog blog)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                var dbEntity = _mapper.Map<BlogEntity>(blog);
                dbContext.Blogs.Add(dbEntity);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(Blog.Domain.Blogs.Blog blog)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                var blogEntity = await dbContext.Blogs.FirstOrDefaultAsync(x => x.Id == blog.Id);
                _mapper.Map(blog, blogEntity);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
