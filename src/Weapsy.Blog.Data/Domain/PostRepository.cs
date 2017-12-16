using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Weapsy.Blog.Data.Entities;
using Weapsy.Blog.Domain.Posts;

namespace Weapsy.Blog.Data.Domain
{
    public class PostRepository : IPostRepository
    {
        private readonly IDbContextFactory _dbContextFactory;
        private readonly IMapper _mapper;

        public PostRepository(IDbContextFactory dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        public async Task<Post> GetByIdAsync(Guid blogId, Guid postId)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                var postEntity = await dbContext.Posts.FirstOrDefaultAsync(x => x.BlogId == blogId && x.Id == postId);
                return postEntity != null ? _mapper.Map<Post>(postEntity) : null;
            }
        }

        public async Task<Guid> GetPostIdByTitleAsync(Guid blogId, Guid postId, string title)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                return await dbContext.Posts.Where(x => x.BlogId == blogId && x.Id == postId && x.Title == title).Select(x => x.Id).FirstOrDefaultAsync();
            }
        }

        public async Task<Guid> GetPostIdBySlugAsync(Guid blogId, Guid postId, string slug)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                return await dbContext.Posts.Where(x => x.BlogId == blogId && x.Id == postId && x.Slug == slug).Select(x => x.Id).FirstOrDefaultAsync();
            }
        }

        public async Task CreateAsync(Post post)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                var postEntity = _mapper.Map<PostEntity>(post);
                dbContext.Posts.Add(postEntity);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(Post post)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                var postEntity = await dbContext.Posts.FirstOrDefaultAsync(x => x.BlogId == post.BlogId && x.Id == post.Id);
                _mapper.Map(post, postEntity);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
