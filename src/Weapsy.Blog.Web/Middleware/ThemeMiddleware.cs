using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Weapsy.Blog.Web.Middleware
{
    public class ThemeMiddleware
    {
        private readonly RequestDelegate _next;

        public ThemeMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext context)
        {
            context.Request.HttpContext.Items[Constants.HttpContextItemThemeKey] = Constants.DefaultTheme;
            return _next(context);
        }
    }
}
