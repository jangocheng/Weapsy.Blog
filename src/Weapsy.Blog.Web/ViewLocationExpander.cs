using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Razor;

namespace Weapsy.Blog.Web
{
    public class ViewLocationExpander : IViewLocationExpander
    {
        public void PopulateValues(ViewLocationExpanderContext context)
        {
            var themeItem = context.ActionContext.HttpContext.Items[Constants.HttpContextItemThemeKey];
            var theme = themeItem != null ? themeItem.ToString() : Constants.DefaultTheme;
            context.Values[Constants.HttpContextItemThemeKey] = theme;
        }

        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            if (context.Values.TryGetValue(Constants.HttpContextItemThemeKey, out var theme))
            {
                viewLocations = new[]
                {
                    $"/Themes/{theme}/Views/{{1}}/{{0}}.cshtml",
                    $"/Themes/{theme}/Views/Shared/{{0}}.cshtml",
                    $"/Themes/{theme}/Areas/{{2}}/Views/{{1}}/{{0}}.cshtml",
                    $"/Themes/{theme}/Areas/{{2}}/Views/Shared/{{0}}.cshtml"
                }
                .Concat(viewLocations);
            }

            return viewLocations;
        }
    }
}
