using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Weapsy.Blog.Data.Entities;

namespace Weapsy.Blog.Data
{
    public class BlogDbContext : IdentityDbContext<UserEntity, RoleEntity, Guid>
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

            builder.Entity<UserEntity>()
                .ToTable("User");

            builder.Entity<RoleEntity>()
                .ToTable("Role");

            builder.Entity<IdentityUserClaim<Guid>>()
                .ToTable("UserClaim");

            builder.Entity<IdentityUserRole<Guid>>()
                .ToTable("UserRole");

            builder.Entity<IdentityUserLogin<Guid>>()
                .ToTable("UserLogin");

            builder.Entity<IdentityUserToken<Guid>>()
                .ToTable("UserToken");

            builder.Entity<IdentityRoleClaim<Guid>>()
                .ToTable("RoleClaim");
        }

        public DbSet<BlogEntity> Blogs { get; set; }
        public DbSet<PostEntity> Posts { get; set; }
    }
}
