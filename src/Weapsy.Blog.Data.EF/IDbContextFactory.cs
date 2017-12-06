namespace Weapsy.Blog.Data.EF
{
    public interface IDbContextFactory
    {
        BlogDbContext CreateDbContext();
    }
}
