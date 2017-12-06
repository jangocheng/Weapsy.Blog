using Microsoft.EntityFrameworkCore;
using Weapsy.Blog.Data.EF.Entities;

namespace Weapsy.Blog.Data.EF
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options)
            : base(options)
        {            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<BlogEntity>()
                .ToTable("Blog");

            builder.Entity<PostEntity>()
                .ToTable("Post");
        }

        public DbSet<BlogEntity> Blogs { get; set; }
        public DbSet<PostEntity> Posts { get; set; }
    }
}
