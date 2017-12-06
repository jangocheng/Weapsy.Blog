using Weapsy.Blog.Domain.Blogs.Commands;
using Weapsy.Blog.Domain.Blogs.Rules;

namespace Weapsy.Blog.Domain.Blogs.CommandHandlers.Validators
{
    public class UpdateBlogValidator : BlogDetailsValidatorBase<UpdateBlog>
    {
        public UpdateBlogValidator(IBlogRules blogRules) : base(blogRules)
        {
            ValidateTitle();
        }
    }
}
