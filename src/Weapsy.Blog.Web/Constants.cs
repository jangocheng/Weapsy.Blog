using System;

namespace Weapsy.Blog.Web
{
    public static class Constants
    {
        public static Guid DefaultBlogId = new Guid("755c30e0-4c5e-4d6c-be89-4e9acee80394");
        public const string DefaultTheme = "Default";
        public const string AdministratorRoleName = "Administrator";
        public const string DefaultEmailAddress = "admin@default.com";
        public const string DefaultPassword = "Ab1234567!";
        public const string HttpContextItemThemeKey = "Theme";
    }
}
