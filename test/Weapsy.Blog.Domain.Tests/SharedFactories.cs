namespace Weapsy.Blog.Domain.Tests
{
    public static class SharedFactories
    {
        public static string RandomString(int length)
        {
            var result = "";
            for (var i = 0; i < length; i++) result += i;
            return result;
        }
    }
}
