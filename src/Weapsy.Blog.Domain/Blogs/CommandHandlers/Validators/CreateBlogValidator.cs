using Weapsy.Blog.Domain.Blogs.Commands;
using Weapsy.Blog.Domain.Blogs.Rules;

namespace Weapsy.Blog.Domain.Blogs.CommandHandlers.Validators
{
    public class CreateBlogValidator : BlogDetailsValidatorBase<CreateBlog>
    {
        public CreateBlogValidator(IBlogRules blogRules) : base(blogRules)
        {
            ValidateTitle();
        }
    }
}
