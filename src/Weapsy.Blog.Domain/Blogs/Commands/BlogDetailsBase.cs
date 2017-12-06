using Weapsy.Mediator.Domain;

namespace Weapsy.Blog.Domain.Blogs.Commands
{
    public abstract class BlogDetailsBase : DomainCommand
    {
        public string Title { get; set; }
    }
}
