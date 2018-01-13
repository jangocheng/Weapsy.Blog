using System;

namespace Weapsy.Blog.Data.Caching
{
    public static class CacheKeys
    {
        public static string IndexCacheKey(Guid blogId)
        {
            return $"Index|{blogId}";
        }

        public static string PostCacheKey(Guid blogId, string slug)
        {
            return $"Post|{blogId}|{slug}";
        }
    }
}
